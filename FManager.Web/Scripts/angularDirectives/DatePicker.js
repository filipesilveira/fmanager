angular.module("FManagerApp")
    .directive('dateTimePicker', function ($rootScope, $parse, $timeout) {

        return {
            require: '?ngModel',
            restrict: 'AE',
            scope: {
                pick12HourFormat: '@',
                language: '@',
                useCurrent: '@',
                location: '@'
            },
            link: function ($scope, $element, $attrs, ngModelCtrl) {
                var passed_in_options = $scope.$eval($attrs.datetimepickerOptions);
                var options = jQuery.extend({}, {locale: 'pt-br'}, passed_in_options);

                function ConvertDate(d) {
                    var days = d.getDate() <= 9 ? ("0" + d.getDate().toString()) : d.getDate().toString();
                    var months = (d.getMonth() + 1) <= 9 ? ("0" + (d.getMonth() + 1).toString()) : (d.getMonth() + 1).toString();
                    var years = d.getFullYear();
                    var hours = d.getHours() <= 9 ? ("0" + d.getHours().toString()) : d.getHours().toString();
                    var minutes = d.getMinutes() <= 9 ? ("0" + d.getMinutes().toString()) : d.getMinutes().toString();
                    var seconds = d.getSeconds() <= 9 ? ("0" + d.getSeconds().toString()) : d.getSeconds().toString();
                    var newDate = years + "-" + months + "-" + days + "T" + hours + ":" + minutes + ":" + seconds;
                    return newDate;
                }

                $element
                    .on('dp.change', function (e) {
                        if (ngModelCtrl) {
                            $timeout(function () {
                                var date = e.date ? ConvertDate(e.date.toDate()) : null;
                                ngModelCtrl.$setViewValue(date);
                            });
                        }
                    })
                    .datetimepicker(options);

                function setPickerValue() {
                    var date = options.defaultDate || null;

                    if (ngModelCtrl && ngModelCtrl.$viewValue && ngModelCtrl.$viewValue !== "Invalid Date") {
                        date = moment(moment(ngModelCtrl.$viewValue, moment.ISO_8601)).format('DD/MM/YYYY HH:mm');
                    }

                    var d = $($element).data('DateTimePicker');

                    if (d && d.date) {
                        d.date(date);
                    }
                }

                if (ngModelCtrl) {
                    ngModelCtrl.$render = function () {
                        setPickerValue();
                    };
                }

                /* Start NaN validation*/
                ngModelCtrl.$parsers.unshift(function (value) {
                    if (value === 'NaN-NaN-NaNTNaN:NaN:NaN') {
                        ngModelCtrl.$setViewValue('');
                        ngModelCtrl.$render();
                        return '';
                    }
                    return value;
                });
                ngModelCtrl.$formatters.unshift(function (value) {
                    if (value === 'NaN-NaN-NaNTNaN:NaN:NaN') {
                        ngModelCtrl.$setViewValue('');
                        ngModelCtrl.$render();
                        return '';
                    }
                    return value;
                });
                /* End Nan validation */

                setPickerValue();
            }
        };
    })
    .directive('datePicker', function ($rootScope, $parse, $timeout) {

        return {
            require: '?ngModel',
            restrict: 'AE',
            scope: {
                pick12HourFormat: '@',
                language: '@',
                useCurrent: '@',
                location: '@'
            },
            link: function ($scope, $element, $attrs, ngModelCtrl) {
                var passed_in_options = $scope.$eval($attrs.datetimepickerOptions);
                var options = jQuery.extend({}, { format: 'MM/DD/YYYY' }, passed_in_options);

                function ConvertDate(d) {
                    var days = d.getDate() <= 9 ? ("0" + d.getDate().toString()) : d.getDate().toString();
                    var months = (d.getMonth() + 1) <= 9 ? ("0" + (d.getMonth() + 1).toString()) : (d.getMonth() + 1).toString();
                    var years = d.getFullYear();
                    var hours = d.getHours() <= 9 ? ("0" + d.getHours().toString()) : d.getHours().toString();
                    var minutes = d.getMinutes() <= 9 ? ("0" + d.getMinutes().toString()) : d.getMinutes().toString();
                    var seconds = d.getSeconds() <= 9 ? ("0" + d.getSeconds().toString()) : d.getSeconds().toString();
                    var newDate = years + "-" + months + "-" + days + "T" + hours + ":" + minutes + ":" + seconds;
                    return newDate;
                }

                function GetFormattedDate(date) {
                    var year = date.getFullYear();

                    var month = (1 + date.getMonth()).toString();
                    month = month.length > 1 ? month : '0' + month;

                    var day = date.getDate().toString();
                    day = day.length > 1 ? day : '0' + day;

                    return month + '/' + day + '/' + year;
                }

                $element.on('change', function (e) {
                    var viewValue = e.target.value;
                    if (options.minDate) {
                        var minDate = new Date(options.minDate);
                        if ((viewValue) && (viewValue.indexOf("T") === -1) && (viewValue.indexOf("/") === -1)) {
                            if (viewValue.length === 8) {
                                var date = moment(viewValue, "MMDDYYYY");
                                if (date.year() < minDate.getFullYear() && ngModelCtrl) {
                                    $timeout(function () {
                                        ngModelCtrl.$setViewValue(ConvertDate(minDate));
                                        e.target.value = GetFormattedDate(minDate);
                                    });
                                }
                            } else {
                                $timeout(function () {
                                    var now = new Date();
                                    ngModelCtrl.$setViewValue(ConvertDate(now));
                                    e.target.value = GetFormattedDate(now);
                                    return;
                                });
                            }
                        }
                        if (viewValue.indexOf("T") > -1) {
                            var _date = viewValue.split('T')[0].split('-');

                            var _year = parseInt(_date[0]);
                            var _month = parseInt(_date[1]) - 1;
                            var _day = parseInt(_date[2]);

                            if (_year < minDate.getFullYear() && ngModelCtrl) {
                                $timeout(function () {
                                    ngModelCtrl.$setViewValue(ConvertDate(minDate));
                                    e.target.value = GetFormattedDate(minDate);
                                });
                            }
                        } else if (viewValue.indexOf("/") > -1) {
                            var _date = viewValue.split('/');
                            if (_date.length < 3) {
                                $timeout(function () {
                                    var now = new Date();
                                    ngModelCtrl.$setViewValue(ConvertDate(now));
                                    e.target.value = GetFormattedDate(now);
                                    return;
                                });
                            }
                            var _year = parseInt(_date[2]);
                            var _month = parseInt(_date[0]) - 1;
                            var _day = parseInt(_date[1]);

                            if (_year < minDate.getFullYear() && ngModelCtrl) {
                                $timeout(function () {
                                    ngModelCtrl.$setViewValue(ConvertDate(minDate));
                                    e.target.value = GetFormattedDate(minDate);
                                });
                            }
                        }
                    }
                });

                $element
                    .on('dp.change', function (e) {
                        if (ngModelCtrl) {
                            $timeout(function () {
                                ngModelCtrl.$setViewValue(ConvertDate(new Date(e.target.value)));
                            });
                        }
                    })
                    .datetimepicker(options);

                function setPickerValue() {
                    var date = options.defaultDate || null;

                    if (ngModelCtrl && ngModelCtrl.$viewValue && ngModelCtrl.$viewValue !== "Invalid Date") {
                        var viewValue = ngModelCtrl.$viewValue;
                        if (options.minDate) {
                            var dtViewValue = new Date(ngModelCtrl.$viewValue);
                            var minDate = new Date(options.minDate);
                            if (minDate > dtViewValue) {
                                viewValue = options.minDate;
                            }
                        }
                        date = moment(moment(viewValue, moment.ISO_8601)).format('MM/DD/YYYY H:mm:ss A');
                    }

                    $($element)
                        .data('DateTimePicker')
                        .date(date);
                }

                if (ngModelCtrl) {
                    ngModelCtrl.$render = function () {
                        setPickerValue();
                    };
                }

                /* Start NaN validation*/
                ngModelCtrl.$parsers.unshift(function (value) {
                    if (value === 'NaN-NaN-NaNTNaN:NaN:NaN') {
                        ngModelCtrl.$setViewValue('');
                        ngModelCtrl.$render();
                        return '';
                    }
                    return value;
                });
                ngModelCtrl.$formatters.unshift(function (value) {
                    if (value === 'NaN-NaN-NaNTNaN:NaN:NaN') {
                        ngModelCtrl.$setViewValue('');
                        ngModelCtrl.$render();
                        return '';
                    }
                    return value;
                });
                /* End Nan validation */

                setPickerValue();
            }
        };
    });