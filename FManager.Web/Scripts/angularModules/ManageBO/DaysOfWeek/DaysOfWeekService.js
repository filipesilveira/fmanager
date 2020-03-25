angular.module("FManagerApp").service("DaysOfWeekService", function () {
    this.data = [
        { DayOfWeekId: 2, Name: "Segunda-feira" },
        { DayOfWeekId: 3, Name: "Terça-feira" },
        { DayOfWeekId: 4, Name: "Quarta-feira" },
        { DayOfWeekId: 5, Name: "Quinta-feira" },
        { DayOfWeekId: 6, Name: "Sexta-feira" },
        { DayOfWeekId: 7, Name: "Sábado" },
        { DayOfWeekId: 1, Name: "Domingo" }
    ];
});