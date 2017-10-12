var koVM = {};
(function (app, $) {
    var hands = [];
    var historyDialog = null;

    var viewModel = {}
    $(function () {
        viewModel = new MainViewModel();
        koVM = viewModel;
        viewModel.Init();

        ko.applyBindings(viewModel);
    });

    var MainViewModel = function () {
        var self = this;
        self.Players = ko.observableArray([]);
        self.Groups = ko.observableArray([]);
        self.WonHands = ko.observableArray([]);
        self.HandHistories = ko.observableArray([]);

        self.IsEvaluateEnabled = ko.observable(false);
        self.IsEvaluated = ko.observable(false);
        self.IsAddNewGroupButtonVisible = ko.observable(false);

        self.SelectedHand = ko.observable('');

        self.API = {
            Hands: '/Home/Hands',
            Players: '/Home/Players',
            WonHands: '/Home/WonHands',
            HandHistory: '/Home/HandHistory',

            SavePlayer: '/Home/SavePlayer',
            SaveWinners: '/Home/SaveWinners'
        };

        self.Init = function () {
            LoadHands();
            LoadPlayers();
            LoadWonHands();
        }

        self.NewPlayer = function () {
            var player = new Player(true);

            self.Players.push(player);
        }

        self.NewGroup = function () {
            if (self.Groups().length < 5) {
                var group = new Group(0, true);
                self.Groups.push(group);
            }
            else {
                alert('Maximum of 5 rows only.');
            }
        }

        self.EvaluateData = function () {
            var forEvaluations = [];
            $.each(self.Groups(), function (index, data) {
                forEvaluations.push({
                    Hand_Id: data.Hand_Id(),
                    Player_Ids: data.PlayerIds()
                });
            });

            var settings = {
                url: self.API.SaveWinners,
                data:  {model: forEvaluations },
                success: function (result) {
                    if (result.IsSuccess) {
                        self.IsEvaluated(true);
                        $.each(self.Groups(), function (index, data) {
                            if (data.Hand_Id() == result.Data) {
                                data.IsWinner(true);
                                LoadWonHands();
                            }
                        });
                    }
                    else {
                        alert(result.ErrorMessage);
                    }
                }
            };

            post_ajax(settings);
        }

        self.NewEvaluation = function () {
            self.Groups.removeAll();
            self.IsEvaluated(false);
        }

        self.ViewHistory = function (data) {
            self.SelectedHand(data.Name);
            LoadHistories(data.Id);
        }

        function LoadHands() {
            var settings = {
                url: self.API.Hands,
                success: function (data) {
                    hands = data;
                    self.IsAddNewGroupButtonVisible(true);
                }
            };

            post_ajax(settings);
        }

        function LoadHistories(handId) {
            var settings = {
                url: self.API.HandHistory,
                data: {handId : handId},
                success: function (data) {
                    self.HandHistories(data);
                    if (historyDialog == undefined) {
                        historyDialog = $('div#hand_history_dialog').modal('show');
                    }
                    else {
                        historyDialog.modal('show');
                    }
                }
            };

            post_ajax(settings);
        }

        function LoadPlayers() {
            var settings = {
                url: self.API.Players,
                success: function (data) {
                    self.Players(data);
                }
            };

            post_ajax(settings);
        }

        function LoadLookUpPlayers() {
            var returnValue;
            var settings = {
                url: self.API.Players,
                async: false,
                success: function (data) {
                    returnValue = data;
                }
            };

            post_ajax(settings);

            return returnValue;
        }

        function LoadWonHands() {
            var settings = {
                url: self.API.WonHands,
                success: function (data) {
                    self.WonHands(data);
                }
            };

            post_ajax(settings);
        }

        var Player = function (isNew) {
            var model = this;
            model.IsSelected = ko.observable(false);
            model.Id = ko.observable();
            model.Name = ko.observable().extend({ required: true });
            model.Genders = ko.observableArray([{ Text: 'M', Value: 'M' }, { Text: 'F', Value: 'F' }]);
            model.Gender = ko.observable();
            model.IsEditing = ko.observable(isNew);

            model.SavePlayer = function (data) {
                if (!isNullOrWhitespace(data.Name())) {
                    var settings = {
                        url: self.API.SavePlayer,
                        data: { Name: data.Name(), Gender: data.Gender() },
                        success: function (result) {
                            if (result.IsSuccess) {
                                data.Id = result.Data;
                                data.IsEditing(false);
                            }
                            else {
                                alert(result.ErrorMessage);
                            }
                        }
                    };

                    post_ajax(settings);
                } else {
                    alert('Player name is required.');
                }
            }
        }

        var Group = function (roundId, isNew) {
            var model = this;

            var players = LoadLookUpPlayers();
            model.Players = ko.observableArray(players);
            model.Hands = ko.observableArray(hands);
            model.Hand_Id = ko.observable(0);
            model.PlayerIds = ko.observableArray();
            model.IsEditing = ko.observable(isNew);
            model.IsWinner = ko.observable(false);

            model.SaveGroup = function (data) {
                var ids = [];
                $.each(data.Players(), function (i, d) {
                    if (d.IsSelected) {
                        ids.push(d.Id);
                    }
                });

                if (ids.length > 0) {
                    data.PlayerIds(ids);
                    data.IsEditing(false);
                    UpdateEvaluateButton();
                }
                else {
                    alert('Please select atleast one player.');
                }
            }

            model.DeleteGroup = function (data) {
                if (!data.IsEditing()) {
                    if (!confirm('Are you sure you want to delete this item?')) return;
                }

                self.Groups.remove(data);
                UpdateEvaluateButton();
            }

            function UpdateEvaluateButton() {
                if (self.Groups().length >= 2 && self.Groups().length <= 5) {
                    self.IsEvaluateEnabled(true);
                }
                else {
                    self.IsEvaluateEnabled(false);
                }
            }
        }
    }
})(this.app, jQuery)