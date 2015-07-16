var createTask = createTask || {};

createTask.createTaskModel = Backbone.Model.extend({
    defaults: {
        Name: ''
    },
    urlRoot: '/Task/CreateTaskModel'
});

createTask.app = Backbone.View.extend({
    el: $('#createTask'),
    events: {
        'click button': 'createTaskButtonClick'
    },
    initialize: function () {
        this.listenTo(Backbone, "ValidationError", this.validationError);
        console.log('dupa2');
    },
    validationError: function (event) {
        if (event.cid == this.cid) {
            console.log(event.Data.Message)
            this.$el.find('.validation-field-error').remove();
            this.$el.find('[name=' + event.Data.Property + ']').after('<span class="validation-field-error">' + event.Data.Message + '</span>')
        }
    },
    createTaskButtonClick: function (e) {
        var cid = this.cid;
        console.log('DUPA');
        var model = new createTask.createTaskModel();
        var self = this.$el;
        for (var name in model.attributes) {
            if (model.attributes.hasOwnProperty(name)) {
                var inputVal = self.find('[name=' + name + ']').val();
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