'use strict';
$(function () {

    $.ajax({
        type: 'GET',
        url: UrlBase + '/Home/DashboardDetails',
        dataType: 'json',
        success: function (result) {
            if (result != undefined) {
                $(".TotalStudents").html(result.TotalStudents);
                $(".TotalPresentStudents").html(result.TotalPresentStudents);
                $(".TotalAbsentStudents").html(result.TotalAbsentStudents);
                $(".TotalLeaveStudents").html(result.TotalLeaveStudents);

                $(".TotalStudentsBar").css("width", (parseInt(parseInt(result.TotalStudents) * 100) / parseInt(result.TotalStudents)) + "%");
                $(".TotalPresentStudentsBar").css("width", (parseInt(parseInt(result.TotalPresentStudents) * 100) / parseInt(result.TotalStudents)) + "%");
                $(".TotalAbsentStudentsBar").css("width", (parseInt(parseInt(result.TotalAbsentStudents) * 100) / parseInt(result.TotalStudents)) + "%");
                $(".TotalLeaveStudentsBar").css("width", (parseInt(parseInt(result.TotalLeaveStudents) * 100) / parseInt(result.TotalStudents)) + "%");

                $(".TotalStaff").html(result.TotalStaff);
                $(".TotalPresentStaff").html("<b>" + result.TotalPresentStaff + "</b>/" + result.TotalStaff);
                $(".TotalAbsentStaff").html("<b>" + result.TotalAbsentStaff + "</b>/" + result.TotalStaff);
                $(".TotalLeaveStaff").html("<b>" + result.TotalLeaveStaff + "</b>/" + result.TotalStaff);

                $(".TotalStaffBar").css("width", (parseInt(parseInt(result.TotalStaff) * 100) / parseInt(result.TotalStaff)) + "%");
                $(".TotalPresentStaffBar").css("width", (parseInt(parseInt(result.TotalPresentStaff) * 100) / parseInt(result.TotalStaff)) + "%");
                $(".TotalAbsentStaffBar").css("width", (parseInt(parseInt(result.TotalAbsentStaff) * 100) / parseInt(result.TotalStaff)) + "%");
                $(".TotalLeaveStaffBar").css("width", (parseInt(parseInt(result.TotalLeaveStaff) * 100) / parseInt(result.TotalStaff)) + "%");

                $(".NewAdmission").html(result.NewAdmission);
                $(".TodayFeeCollection").html(result.TodayFeeCollection);
                $(".TotalFeePending").html(result.TotalFeePending);
                var HTMLStr = "";
                if (result.StudentList != null) {
                    for (var i = 0; i < result.StudentList.length; i++) {
                        HTMLStr += "<li>";
                        HTMLStr += "<img src='" + result.StudentList[i].StudentImage + "' alt='Image' style='height:60px; width: 60px;' />";
                        HTMLStr += '<a class="users-list-name" href="#" onclick="return DisplayAdmissionForm(\'' + result.StudentList[i].StudentID + '\')">' + result.StudentList[i].StudentName + '</a>';
                        HTMLStr += "<span class='users-list-date'>" + result.StudentList[i].AdmissionDate + "</span>";
                        HTMLStr += "</li>";
                    }
                    $(".NewStudentList").html(HTMLStr);
                }
                drawColumnChart(result.ChartData);
            }
            else {
                $(".TotalStudents").html("NA");
                $(".TotalPresentStudents").html("NA");
                $(".TotalAbsentStudents").html("NA");
                $(".TotalLeaveStudents").html("NA");

                $(".TotalStaffBar").html("NA");
                $(".TotalPresentStaff").html("NA");
                $(".TotalAbsentStaff").html("NA");
                $(".TotalLeaveStaff").html("NA");

                $(".NewAdmission").html("NA");
                $(".TodayFeeCollection").html("NA");
                $(".TotalFeePending").html("NA");
            }
        },
        error: function (ex) {
        }
    });

    google.charts.load('current', { 'packages': ['bar', 'corechart'] });

    function drawColumnChart(dataValues) {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Month');
        data.addColumn('number', 'Fee');

        for (var j = 0; j < dataValues.length; j++) {
            data.addRow([dataValues[j].Year + '-' + dataValues[j].Month, parseInt(dataValues[j].MonthFee)]);
        }
        var options = {
            is3D: true,
            title: 'Motivation Level Throughout the Day',
            hAxis: {
                title: 'Time of Day',
                format: 'h:mm a',
                viewWindow: {
                    min: [7, 30, 0],
                    max: [17, 30, 0]
                }
            },
            vAxis: {
                title: 'Rating (scale of 1-10)'
            }
        };
        new google.visualization.ColumnChart(document.getElementById('divColumnChart')).

        draw(data, { title: "", options });
    }

});