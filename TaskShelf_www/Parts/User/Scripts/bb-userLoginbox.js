var userLoginbox = userLoginbox || {};

userLoginbox.loginModel = Backbone.Model.extend({
    defaults: {
        Login: '',
        Password: ''
    },
    urlRoot: '/User/Login'
});

userLoginbox.app = Backbone.View.extend({
    el: $('#userLoginbox'),
    events: {
        'click button': 'loginButtonClick'
    },
    initialize: function () {
        this.listenTo(Backbone, "ValidationError", this.validationError);
    },
    validationError: function (event) {
        console.log('val');
        if (event.cid == this.cid) {
            this.$el.find('.validation-field-error').remove();
            this.$el.find('[name=' + event.Data.Property + ']').after('<span class="validation-field-error">' + event.Data.Message + '</span>')
        }
    },
    loginButtonClick: function (e) {
        var cid = this.cid;
        var model = new userLoginbox.loginModel();
        var el = this.$el;
        for (var name in model.attributes) {
            if (model.attributes.hasOwnProperty(name)) {
                var inputVal = el.find('[name=' + name + ']').val();
                model.set(name, inputVal);
            }
        }
        model.save(null, {
            success: function (model, response) {
                response.cid = cid;
                eventHandlers.publish(response);
            }, error: function (model, response) {
                var event = jQuery.parseJSON(response.responseText);
                event.cid = cid;
                eventHandlers.publish(event);
            }
        });
    }
})