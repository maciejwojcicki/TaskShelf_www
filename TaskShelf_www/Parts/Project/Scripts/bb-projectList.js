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
            ProjectId: null,
            Name: "",
            CreateDate: null,
            Owner: "",
        Description: "",
        Url: ""
    }
});

projectList.projectView = Backbone.View.extend({
    events:{
        'click a': 'projectClick'
    },
    initialize: function () {
        this.template = _.template($('#projectView-template').html());
    },
    projectClick: function(){
        var model = this.model;
        console.log(model.get('ProjectId'));

        var now = new Date();
        var time = now.getTime;
        var expireTime = time + 1000 * 36000;
        now.setTime(expireTime);
        document.cookie = 'ProjectId=' + model.get('ProjectId') + ';expires=' + now.toGMTString() + ';path=/';

        console.log(document.cookie)
        window.location.href = model.get('Url');
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