var labelList = labelList || {}

labelList.labelListModel = Backbone.Model.extend({
    defaults: {
        Labels: new Array(),
        HasMore: false
    },
    urlRoot: '/Project/LabelList'
});

labelList.labelModel = Backbone.Model.extend({
    defaults: {
        Name: ''
    }
});

labelList.labelView = Backbone.View.extend({
    initialize: function () {
        this.template = _.template($('#labelView-template').html());
    },
    render: function () {
        var self = this;
        this.$el.html(this.template(this.model.toJSON()));

        return this;
    }
});

labelList.app = Backbone.View.extend({
    el: $('#labelList'),
    events: {

    },
    initialize: function () {
        var self = this;
        this.model = new labelList.labelListModel();
        this.initialLoad();
    },
    initialLoad: function () {
        var self = this;
        var labelListModel = new labelList.labelListModel();
        labelListModel.fetch({
            data: {
                page: 1,
                count: 5
            },

            success: function () {
                var labels = labelListModel.get('Labels');
                console.log(labels)
                _.each(labels, function (label) {
                    var labelModel = new labelList.labelModel(label);
                    var labelView = new labelList.labelView({ model: labelModel });
                    self.$el.find('div.labels-container').append(labelView.render().el)
                });
                if (labelListModel.get('HasMore')) {
                    self.$el.find('button.show-more').show();
                } else {
                    self.$el.find('button.show-more').hide();
                }
            },
            error: function (response) {
                var event = jQuery.parseJSON(response.responseText);
                event.cid = 'labelList';
                eventHandlers.publish(event);
            }
        })
    },

})