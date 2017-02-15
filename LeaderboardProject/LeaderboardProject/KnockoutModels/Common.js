function Section(name) {
    var self = this;

    self.name = ko.observable(name);
    self.scores = ko.observableArray([]);
}
function Leaderboard(Id, GamerName, Day, Score, games) {
    var self = this;

    // observable are update elements upon changes, also update on element data changes [two way binding]
    self.Id = ko.observable(Id);
    if (games != null && games.Name != null) self.Game = ko.observable(games.Name);
    else self.Game = "";
    self.GamerName = ko.observable(GamerName);
    self.Day = ko.observable(Day);
    self.Score = ko.observable(Score);
};

function LeaderboardList() {
    var self = this;

    // observable arrays are update binding elements upon array changes
    self.sections = ko.observableArray([]);
    self.selected = ko.observable();
  

    var tmp = new self.sections();
    tmp.push(new Section("All"));
    tmp.push(new Section("Day"));
    tmp.push(new Section("Week"));
    tmp.push(new Section("Year"));

    self.toogleSelected = function (index) {
        self.getLeaderboard(index);
        self.selected(self.sections()[index()]);
    }

   self.getLeaderboard = function (index) {
        if (self.sections()[index()].scores().length == 0) {
            //self.selectedSection.scores.removeAll();

            $.getJSON('/api/leaderboard/' + index(), function (data) {
                console.log("success");
                $.each(data, function (key, value) {
                    self.sections()[index()].scores.push(new Leaderboard(value.Id, value.GamerName, value.Day, value.Score, value.games));
                });
            })
            .done(function () {
                console.log("second success")
            })
            .fail(function () { console.log("fail") })
            .always(function () { console.log("complete") });
        }

        // обновляем представление каждый раз на случай ошибок
        self.sections.valueHasMutated();
    };
};

function Game(Id, Name) {
    var self = this;

    // observable are update elements upon changes, also update on element data changes [two way binding]
    self.Id = ko.observable(Id);
    self.Name = ko.observable(Name);
};

function GamesList() {
    var self = this;

    // observable arrays are update binding elements upon array changes
    self.games = ko.observableArray([]);

    self.getGames = function () {
        self.games.removeAll();

        $.getJSON('/api/games', function (data) {
            console.log("success");
            self.games.push(new Game(null, 'All games'));
            $.each(data,
               function (key, value) {
                   self.games.push(new Game(value.Id, value.Name));
               });
            console.log(self.games);
        })
        .done( function () { console.log("second success") } )
        .fail(function (obj, textStatus, error) {
            console.log("fail" + textStatus + "," + error)
        })
        .always(function () { console.log("complete") });

        // обновляем представление каждый раз на случай ошибок
        self.games.valueHasMutated();
    };
};

//viewModel = { gamesListViewModel:, leaderboardListViewModel:  };

var gamesListViewModel = new GamesList();
var leaderboardListViewModel = new LeaderboardList();

// on document ready
$(document).ready(function () {
    // bind view model to referring view
    ko.applyBindings(gamesListViewModel, document.getElementById('games_select'));
    gamesListViewModel.getGames();

    leaderboardListViewModel.selected(leaderboardListViewModel.sections()[0]);
    ko.applyBindings(leaderboardListViewModel, document.getElementById('myTabs'));
});