var createTask = createTask || {};

createTask.createTaskModel = Backbone.Model.extend({
    defaults: {
        Name: "",
        dupa:"dupa",
        Description: "",
        ExpectedWorkTime: null,
        Type: null,
        Attachments: new Array()
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
    newAttachment: function(event){
        var attachmentView = new createTask.attachmentView();
        this.$el.find('div.attachments-container').append(attachmentView.render().el)
    },
    inputTextChange: function (e) {
        var text = $(e.target).val();
        var name = $(e.target).attr('name');
        this.model.set(name, text);
        console.log(name)
        console.log(text);
    },
    inputFileChange: function (e) {
        var name = $(e.target).attr('name');
        var _array = this.model.get(name);
        _array.push($(e.target).val().split('\\').pop());
        this.model.set(name, _array);
        console.log(this.model)
    },
    createTaskButtonClick: function (e) {
        var cid = this.cid;
        var self = this.$el;

        this.model.save(null, {
            success: function (model, response) {
                response.cid = cid;
                eventHandlers.publish(response);
                console.log(model);
                console.log(response);
            }, error: function (model, response) {
                var event = jQuery.parseJSON(response.responseText);
                event.cid = cid;
                eventHandlers.publish(event);
                console.log(model);
                console.log(response);
            }
        });
    }
});