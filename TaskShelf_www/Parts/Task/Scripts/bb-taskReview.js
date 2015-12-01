var taskReview = taskReview || {};

taskReview.attachmentModel = Backbone.Model.extend({
    defaults: {
        AttachmentId: null,
        FileName: ''
    }
});

taskReview.attachments = Backbone.Collection.extend({
    model: taskReview.attachmentModel
})

taskReview.commentModel = Backbone.Model.extend({
    defaults: {
        CommentId: null,
        Text: ''
    }
});

taskReview.checkList = Backbone.Collection.extend({
    model: taskReview.checkListModel
})

taskReview.checkListModel = Backbone.Model.extend({
    defaults: {
        CheckListId: null,
        Text: '',
        Done: false
    }
});

taskReview.labels = Backbone.Collection.extend({
    model: taskReview.labelModel
})

taskReview.labelModel = Backbone.Model.extend({
    defaults: {
        LabelId: null,
        LabelName: ''
    }
});

taskReview.comments = Backbone.Collection.extend({
    model: taskReview.commentModel
})

taskReview.taskReviewModel = Backbone.Model.extend({
    defaults: {
        TaskId: null,
        Name: '',
        Description: '',
        CreateDate: '',
        ExpectedWorkTime: 0,
        CompletedDate: null,
        Status: '',
        Type: '',
        Attachments: new taskReview.attachments(),
        CheckList: new taskReview.checkList(),
        Comments: new taskReview.comments(),
        Labels: new taskReview.labels()
    },
    urlRoot: '/Task/TaskReviewModel/'
});

taskReview.taskView = Backbone.View.extend({
    initialize: function () {
        this.template = _.template($('#taskView-template').html());
    },
    render: function () {
        var self = this;

        this.$el.html(this.template(this.model.toJSON()));

        return this;
    }
});

taskReview.attachmentView = Backbone.View.extend({
    initialize: function () {
        this.template = _.template($('#attachmentView-template').html());
    },
    render: function () {
        var self = this;
        this.$el.html(this.template(this.model.toJSON()));

        return this;
    }
});
taskReview.labelView = Backbone.View.extend({
    initialize: function () {
        this.template = _.template($('#labelView-template').html());
    },
    render: function () {
        var self = this;
        this.$el.html(this.template(this.model.toJSON()));

        return this;
    }
});

taskReview.labelDropView = Backbone.View.extend({
    initialize: function () {
        this.template = _.template($('#labelDropView-template').html());
    },
    render: function () {
        var self = this;
        this.$el.html(this.template(this.model.toJSON()));

        return this;
    }
});
taskReview.commentView = Backbone.View.extend({
    initialize: function () {
        this.template = _.template($('#commentView-template').html());
    },
    render: function () {
        var self = this;
        this.$el.html(this.template(this.model.toJSON()));

        return this;
    }
});

taskReview.app = Backbone.View.extend({
    el: $('#taskReview'),
    events: {

    },
    initialize: function () {
        var self = this;
        self.model = new taskReview.taskReviewModel();

        this.initialLoad();
    },
    initialLoad: function () {
        var self = this;

        self.model.fetch({
            data: $.param({
                taskId: self.options.TaskId
            }),
            success: function () {
                var taskView = new taskReview.taskView({ model: self.model });
                console.log(self.$el.find('div.task-container'))
                self.$el.find('div.task-container').append(taskView.render().el)
                
                _.each(self.model.get('Attachments'), function (attachment) {
                    var attachmentModel = new taskReview.attachmentModel(attachment);
                    var attachmentView = new taskReview.attachmentView({ model: attachmentModel });
                    self.$el.find('div.attachment-container').append(attachmentView.render().el)
                });

                _.each(self.model.get('Labels'), function (label) {
                    var labelModel = new taskReview.labelModel(label)
                    var labelView = new taskReview.labelView({ model: labelModel });
                    self.$el.find('div.label-container').append(labelView.render().el)
                });

                _.each(self.model.get('Comments'), function (comment) {
                    var commentModel = new taskReview.commentModel(comment)
                    var commentView = new taskReview.commentView({ model: commentModel });
                    self.$el.find('div.comment-template').append(commentView.render().el)

                });

                _.each(self.model.get('Labels'), function (label) {
                    var labelModel = new taskReview.labelModel(label)
                    var labelView = new taskReview.labelDropView({ model: labelModel });
                    self.$el.find('select.label', self.el).append(labelView.render().el)
                });
            },
            error: function (response) {
                var event = jQuery.parseJSON(response.responseText);
                event.cid = 'taskReview';
                eventHandlers.publish(event);
            }
        });
    }
})