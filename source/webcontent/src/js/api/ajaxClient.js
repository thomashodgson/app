var ClientSide = require("../view-model-updates/ClientSide.js");

var client = {
    post: function (url, data) {
        return doAjax(url, JSON.stringify(data));
    },
    ping: function(url) {
        return doAjax(url, "");
    }
}

var doAjax = function (url, data) {
    return $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            data: data,
            error: function(xhr, _, error) {
                if (xhr.status === 401) {
                    ClientSide.updateExistingViewModel({ User: null, UserAuthenticationFinished: true });
                } else {
                    console.log(error);
                }
            }
        }
    );
};

module.exports = client