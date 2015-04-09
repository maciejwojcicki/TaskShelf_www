var eventHandlers = eventHandlers || {};

eventHandlers.publish = function (event) {
    if ($.isArray(event)) {
        $(event).each(function () {
            console.log($(this)[0]);
            Backbone.trigger($(this)[0].Type, $(this)[0]);
        })
    } else {
        console.log($(event)[0]);
        Backbone.trigger($(event)[0].Type, $(event)[0]);
    }
};

eventHandlers.errorMessageModel = Backbone.Model.extend({
    defaults: {
        Type: '',
        Data: new Object()
    }
});

eventHandlers.infoMessageModel = Backbone.Model.extend({
    defaults: {
        Type: '',
        Data: new Object()
    }
});

eventHandlers.errorMessageView = Backbone.View.extend({
    initialize: function () {
        this.template = _.template($('#error-message-template').html());
    },
    render: function () {
        this.$el.html(this.template(this.model.toJSON()));
        return this;
    }
});

eventHandlers.infoMessageView = Backbone.View.extend({
    initialize: function () {
        this.template = _.template($('#info-message-template').html());
        var self = this;
        setTimeout(function () {
            self.$el.fadeOut(3000, function () {
                self.remove();
            });
        }, 3000);
    },
    render: function () {
        this.$el.html(this.template(this.model.toJSON()));
        return this;
    }
});

eventHandlers.app = Backbone.View.extend({
    initialize: function () {
        this.listenTo(Backbone, 'Error', this.onError, this);
        this.listenTo(Backbone, 'Info', this.onInfo, this);
        this.listenTo(Backbone, 'Redirect', this.onRedirect, this);
    },
    onError: function (event) {
        var model = new eventHandlers.errorMessageModel(event);
        var view = new eventHandlers.errorMessageView({ model: model });
        $('#error-messages').append(view.render().el);
    },
    onInfo: function (event) {
        var model = new eventHandlers.infoMessageModel(event);
        var view = new eventHandlers.infoMessageView({ model: model });
        $('#info-messages').append(view.render().el);
    },
    onRedirect: function (event) {

        top.location = event.Data.Url;
    }
});

new eventHandlers.app();