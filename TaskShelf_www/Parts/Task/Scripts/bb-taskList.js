var taskList = taskList || {};

taskList.taskListModel = Backbone.Model.extend({
    defaults: {
        Tasks: new Array(),
        HasMore: false
    },
    urlRoot: '/Task/TaskList'
});

taskList.taskModel = Backbone.Model.extend({
    defaults: {
        Name: ''
    }
});

taskList.taskView = Backbone.View.extend({
    initialize: function () {
        this.template = _.template($('#taskView-template').html());
    },
    render: function () {
        var self = this;
        this.$el.html(this.template(this.model.toJSON()));

       

        return this;
    }
});

taskList.app = Backbone.View.extend({
    el: $('#taskList'),
    events: {

    },
    initialize: function () {
        var self = this;
        this.model = new taskList.taskListModel();
        console.log(document.cookie);
        //console.log("dupa");
        //this.$el.find('.taskList').html(
        //    new taskList.taskView().render().el)
        this.initialLoad();
    },
    initialLoad: function () {
        var self = this;
        var taskListModel = new taskList.taskListModel();
        taskListModel.fetch({
            data: {
                page: 1,
                count: 5 
            },
            success: function () {
                var tasks = taskListModel.get('Tasks');
                console.log(tasks)
                _.each(tasks, function (task) {
                    var taskModel = new taskList.taskModel(task);
                    var taskView = new taskList.taskView({ model: taskModel });
                    self.$el.find('div.tasks-container').append(taskView.render().el)
                });
                if (taskListModel.get('HasMore')) {
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
    },
})