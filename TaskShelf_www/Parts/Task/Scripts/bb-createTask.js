var createTask = createTask || {};

createTask.attachmentModel = Backbone.Model.extend({
    defaults: {
        FileName: ""
    }
})
createTask.attachmentsList = Backbone.Collection.extend({
    model: createTask.attachmentModel
})

createTask.createTaskModel = Backbone.Model.extend({
    defaults: {
        Name: "",
        Description: "",
        ExpectedWorkTime: null,
        Type: null,
        Attachments: new createTask.attachmentsList()
    },
    urlRoot: '/Task/CreateTask'
});



createTask.attachmentView = Backbone.View.extend({
    initialize: function () {
        this.template = _.template($('#attachmentView-template').html());
    },
    render: function () {
        var self = this;
        this.$el.html(this.template());

        return this;
    }
});

createTask.app = Backbone.View.extend({
    el: $('#createTask'),
    events: {
        'click button.create': 'createTaskButtonClick',
        'change input[type="text"]': 'inputTextChange',
        'change input[type="text"].array': 'inputTextArrayChange',
        'change textarea': 'inputTextChange',
        'change select': 'inputTextChange',
        'change input[type="file"]': 'inputFileChange',
        'click .newAttachment': 'newAttachment'
    },
    initialize: function () {
        var self = this;
        self.attachments = new createTask.attachmentsList();
        self.model = new createTask.createTaskModel();
        this.listenTo(Backbone, "ValidationError", this.validationError);
        var attachmentView = new createTask.attachmentView();
        this.$el.find('div.attachments-container').append(attachmentView.render().el)
    },
    validationError: function (event) {
        if (event.cid == this.cid) {
            this.$el.find('.validation-field-error').remove();
            this.$el.find('[name=' + event.Data.Property + ']').after('<span class="validation-field-error">' + event.Data.Message + '</span>')
        }
    },
    newAttachment: function (event) {
        var attachmentView = new createTask.attachmentView();
        this.$el.find('div.attachments-container').append(attachmentView.render().el)
    },
    inputTextChange: function (e) {
        var text = $(e.target).val();
        var name = $(e.target).attr('name');
        this.model.set(name, text);

    },
    inputFileChange: function (e) {
        this.attachments.add([{ FileName: $(e.target).val().split('\\').pop() }])
        //attachment.set('FileName', $(e.target).val().split('\\').pop());
        console.log(this.attachments)
        
        this.model.set('Attachments', this.attachments);

        console.log(this.model)
        //var name = $(e.target).attr('name');
        //var array = this.model.get(name);
        //array.models.push($(e.target).val().split('\\').pop());
        //console.log(array.models)
        //this.model.set(name, array.models);
        //console.log(this.model)

    },
    createTaskButtonClick: function (e) {
        var cid = this.cid;
        var self = this.$el;
        console.log(this.model)
        this.model.save(null, {
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
