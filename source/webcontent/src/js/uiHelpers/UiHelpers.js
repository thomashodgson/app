module.exports = {
    combineClasses: function (a) {
        var stringArray = _.isArray(a) ? a : Array.prototype.slice.call(arguments);
        return _.compact(stringArray).join(" ");
    },

    getPluralisedCountString: function(word, count) {
        var plural = count !== 1 ? "s" : "";
        return count + " " + word + plural;
    }
}