var ClientSide = require('./ClientSide.js');
var AjaxClient = require('../api/AjaxClient.js');

var mockServerRequests = false;

var createViewModelFragment = function (key, object) {
    var newFragment = {};
    newFragment[key] = object;
    return newFragment;
}

var setViewModelLoading = function(name) {
    ClientSide.updateExistingViewModel(createViewModelFragment(name, {
        IsLoading: true
    }));
}

var inASecond = function (viewModelFragment) {
    setTimeout(function () {
        ClientSide.updateExistingViewModel(viewModelFragment);
    }, 1000);
};

var real = {
    sayHello: function (name) {
        setViewModelLoading("HelloMessageViewModel");
        AjaxClient.post('api/queue/Hello', { Name: name });
    },
    
    sayBye: function (name) {
        setViewModelLoading("HelloMessageViewModel");
        AjaxClient.post('api/queue/Bye', { Name: name });
    }
};

var fake = {
    sayHello: function (name) {
        var viewModelKey = "HelloMessageViewModel";
        setViewModelLoading(viewModelKey);
        inASecond(createViewModelFragment(viewModelKey, {
            Message: "Fake Hello " + name,
            IsLoading: false
        }));
    }
};

module.exports = mockServerRequests ? fake : real;
