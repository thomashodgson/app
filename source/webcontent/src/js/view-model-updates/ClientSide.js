
module.exports = {
    initialize:  function (setState, replaceState) {
        this.setState = setState;
        this.replaceState = replaceState;
    },

    updateExistingViewModel: function (viewModelFragment) {
        this.setState(viewModelFragment);
    },

    createNewViewModel: function (viewModel) {
        this.replaceState(viewModel);
    }
}