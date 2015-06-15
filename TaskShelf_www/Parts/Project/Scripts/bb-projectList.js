var projectList = projectList || {};

projectList.projectListModel = Backbone.Model.extend({
    defaults: {
        Projects: new Array(),
        HasMore: false
    },
    urlRoot: '/Project/ProjectList'
});

projectList.projectModel = Backbone.Model.extend({
    defaults: {
        Name: "",
        CreateDate: null,
        Owner: "",
        Description: ""
    }
});

projectList.projectView = Backbone.View.extend({
    initialize: function () {
        this.template = _.template($('#projectView-template').html());
    },
    render: function () {
        var self = this;
        this.$el.html(this.template(this.model.toJSON()));

        return this;
    }
});

projectList.app = Backbone.View.extend({
    el: $('#projectList'),
    events: {

    },
    initialize: function () {
        var self = this;
        this.model = new projectList.projectListModel();

        //console.log("dupa");
        //this.$el.find('.projectList').html(
        //    new projectList.projectView().render().el)
        this.initialLoad();
    },
    initialLoad: function () {
        var self = this;
        var projectListModel = new projectList.projectListModel();
        projectListModel.fetch({
            data: {
                page: 1,
                count: 5
            },
            success: function () {
                var projects = projectListModel.get('Projects');
                console.log(projects)
                _.each(projects, function (project) {
                    var projectModel = new projectList.projectModel(project);
                    var projectView = new projectList.projectView({ model: projectModel });
                    self.$el.find('div.projects-container').append(projectView.render().el)
                });

                if (projectListModel.get('HasMore')) {
                    self.$el.find('button.show-more').show();
                } else {
                    self.$el.find('button.show-more').hide();
                }
            },
            error: function (response) {
                var event = jQuery.parseJSON(response.responseText);
                event.cid = 'projectList';
                eventHandlers.publish(event);
            }
        })
    },
})