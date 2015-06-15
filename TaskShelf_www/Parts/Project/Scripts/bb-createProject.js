var createProject = createProject || {};

createProject.createProjectModel = Backbone.Model.extend({
    defaults: {
        Name: '',
        ImageThumbnail: ''
    },
    urlRoot: '/Project/CreateProject'
});

createProject.app = Backbone.View.extend({
    el: $('#createProject'),
    events: {
        'click button': 'createProjectButtonClick'
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
    createProjectButtonClick: function (e) {
        var cid = this.cid;
        var model = new createProject.createProjectModel();
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