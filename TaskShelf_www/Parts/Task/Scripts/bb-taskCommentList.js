var taskComment = taskComment || {};

taskComment.taskCommentModel = Backbone.Model.extend({
    defaults: {
        TaskComments: new Array(),
        HasMore: false
    },
    urlRoot: '/Task/CommentsList'
});

taskComment.taskModel = Backbone.Model.extend({
    defaults: {
        TaskCommentId: null,
        Text: ''
    }
});

taskComment.taskView = Backbone.View.extend({
    initialize: function () {
        this.template = _.template($('#taskView-template').html());
    },
    render: function () {
        var self = this;
        this.$el.html(this.template(this.model.toJSON()));



        return this;
    }
});

taskComment.app = Backbone.View.extend({
    el: $('taskComment'),
    events: {

    },
    initialize: function () {
        var self = this;
        this.model = new taskComment.taskCommentModel();
        console.log(this.model)
        this.initialLoad();
    },
    initialLoad: function () {
        var self = this;
        self.model.fetch({
            data: {
                page: 1,
                count: 5
            },
            success: function () {
                var tasks = self.model.get('TaskComments');
                console.log(tasks)
                _.each(tasks, function (task) {
                    var taskModel = new taskComment.taskModel(task);
                    var taskView = new taskComment.taskView({ model: taskModel });
                    self.$el.find('div.tasks-container').append(taskView.render().el)
                });
                if (self.model.get('HasMore')) {
                    self.$el.find('button.show-more').show();
                } else {
                    self.$el.find('button.show-more').hide();
                }
            },
            error: function (response) {
                var event = jQuery.parseJSON(response.responseText);
                event.cid = 'taskList';
                eventHandlers.publish(event);
            }
        })
    }
})