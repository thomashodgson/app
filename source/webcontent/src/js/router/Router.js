var ClientSideViewModelUpdates = require('../view-model-updates/ClientSide.js');
var XRegExp = require('xregexp');
var React = require('react');
var _ = require('underscore');


module.exports = {
 
    initialize: function(routes) {
        this.routes = routes;
        this.listenForRouteChanges();
        this.initialized = true;
    },

    isRouteMatch: function(state, route) {
        return route.page === state.Page && _.isEmpty(_.difference(this.getCaptureNames(route), _.keys(state)));
    },

    insertCapture: function(state, segment) {
        return this.isCaptureSegment(segment) ? state[this.getCaptureName(segment)] : segment;
    },

    updateUrl: function(state, route) {
        var hash = _.map(this.getRouteSegments(route), _.bind(this.insertCapture, this, state)).join("/");
        location.hash = '#' + hash;
    },

    updateUrlHash: function(state) {
        for (var i = 0; i < this.routes.length; ++i) {
            var route = this.routes[i];
            if (!route.disableUrlUpdates && this.isRouteMatch(state, route)) {
                this.updateUrl(state, route);
                return;
            }
        }
    },

    getRouteSegments: function(route) {
        return route.pattern.split("/");
    },

    isCaptureSegment: function(segment) {
        return segment[0] === ":";
    },

    getCaptureNames: function(route) {
        return _.map(this.getCaptures(route), this.getCaptureName);
    },

    getCaptures: function(route) {
        return _.filter(this.getRouteSegments(route), this.isCaptureSegment);
    },

    getCaptureName: function(captureSegment) {
        return captureSegment.substring(1);
    },

    toRegExpSegment: function(segment) {
        return this.isCaptureSegment(segment) ? ("(?<" + this.getCaptureName(segment) + ">[^/]*)") : segment;
    },

    toRegExp: function(route) {
        return _.map(this.getRouteSegments(route), this.toRegExpSegment, this).join("/");
    },

    listenForRouteChanges: function() {
        window.addEventListener('hashchange', _.bind(this.updateViewModelFromHash, this));
    },

    getHash: function() {
        return window.location.hash.substr(1);
    },

    updateViewModelFromHash: function () {
        var newHash = this.getHash();
        var viewModelUpdate = this.findViewModelUpdateFromRoute(newHash);

        if (viewModelUpdate) {
            ClientSideViewModelUpdates.updateExistingViewModel(viewModelUpdate);
        } 
    },

    createViewModelUpdate: function(route, regExpMatch) {
        var result = {
            Page: route.page
        };

        var captureNames = this.getCaptureNames(route);

        _.each(captureNames, function(key) {
            result[key] = regExpMatch[key];
        });

        return result;
    },

    findViewModelUpdateFromRoute: function(hash) {
        for (var i = 0; i < this.routes.length; ++i) {
            var route = this.routes[i];
            var routeRegExp = this.toRegExp(route);
            var regExp = XRegExp(routeRegExp, 'gi');
            var result = XRegExp.exec(hash, regExp);
            if (result && result.length) {
                return this.createViewModelUpdate(route, result);
            }
        }
    },

    getView: function(state) {
        for (var i = 0; i < this.routes.length; ++i) {
            var route = this.routes[i];
            if (this.isRouteMatch(state, route)) {
                return route.getView(state);
            }
        }
    },

    getViewModelFromUrl: function() {
        var hash = this.getHash();
        return this.findViewModelUpdateFromRoute(hash);
    }
};