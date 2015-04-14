var userRegisterbox = userRegisterbox || {};

userRegisterbox.registerModel = Backbone.Model.extend({
    defaults: {
        Login: '',
        Password: '',
        ConfirmPassword: '',
        Name: '',
        Email: '',
        ActivationToken: ''
    },
    urlRoot: '/User/RegisterUser'
});

userRegisterbox.app = Backbone.View.extend({
    el: $('#userRegisterbox'),
    events: {
        'click button': 'registerButtonClick'
    },
    initialize: function () {
        this.listenTo(Backbone, "ValidationError", this.validationError);
    },
    validationError: function (event) {
        if (event.cid == this.cid) {
            this.$el.find('.validation-field-error').remove();
            this.$el.find('[name=' + event.Data.Property + ']').after('<span class="validation-field-error">' + event.Data.Message + '</span>')
        }
    },
    registerButtonClick: function (e) {
        var cid = this.cid;
        var model = new userRegisterbox.registerModel();
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
});