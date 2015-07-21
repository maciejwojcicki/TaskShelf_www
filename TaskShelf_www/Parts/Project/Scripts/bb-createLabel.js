var createLabel = createLabel || {};

createLabel.createLabelModel = Backbone.Model.extend({
    defaults: {
        Name: ''
    },
    urlRoot: '/Project/CreateLabel'
});

createLabel.app = Backbone.View.extend({
    el: $('#createLabel'),
    events: {
        'click button': 'createLabelButtonClick'
    },

    initialize: function () {
        this.listenTo(Backbone, "ValidationError", this.validationError);
    },
    validationError: function (event) {
        if (event.cid == this.cid) {
            this.$el.find('.validation-field-error').remove();
            this.$el.find('[name='+event.Data.Property+']').after('<span class="validation-field-error">'+event.Data.Message+'</span>')
        }
    },
    createLabelButtonClick: function (e) {
        var cid = this.cid;
        console.log("cid");
        console.log(cid);
        var model = new createLabel.createLabelModel();
        var el = this.$el;
        console.log("el");
        console.log(el);
        for (var name in model.attributes) {
            if (model.attributes.hasOwnProperty(name)) {
                var inputVal = el.find('[name=' + name + ']').val();
                model.set(name, inputVal);
                console.log("Name");
                console.log(name);
                console.log("InputVal");
                console.log(inputVal);
            }
        }
        model.save(null, {
            success: function (model, response) {
                response.cid = cid;
                console.log("Cid");
                console.log(cid);
                eventHandlers.publish(response);
            }, error: function (model, response) {
                var event = jQuery.parseJSON(response.responseText);
                event.cid = cid;
                eventHandlers.publish(event);
            }
        })
    }
})