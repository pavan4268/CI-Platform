﻿$(document).ready(function () {
    
    GetCountry();
    GetMissionThemes();
    GetMissionSkillList();
    $('#CountryList').change(function () {
        var id = $(this).val();
        $('#CityList').empty();
        $('#CityList').append('<Option>City</Option>');
        $.ajax({
            url: '/Mission/City?id=' + id,
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#CityList').append('<Option value =' + data.cityId + '>' + data.name + '</Option>');
                });
            }
        });
    });
    //$('#searchButton'.on("click", function () {
    //    var inputvalue = $("#InputField").val();
    //    $.ajax({
    //        url: controller / IDBTransaction
    //        data: { controller_parameter: inputvalue }
    //        success: function (result) {

    //        }
    //    })
    //})

    //})

});


function GetCountry(){
    $.ajax({
        url: '/Mission/Country',
        success: function (result) {
            $.each(result, function (i, data) {
                $('#CountryList').append('<Option value=' + data.countryId + '>' + data.name + '</Option>');
            });
        }
    });
}

function GetMissionThemes() {
    $.ajax({
        url: '/Mission/Theme',
        success: function (result) {
            $.each(result, function (i, data) {
                $('#MissionThemeList').append('<Option value=' + data.missionThemeId + '>' + data.title + '</Option>');
            });
        }
    });
}


function GetMissionSkillList() {
    $.ajax({
        url: '/Mission/Skills',
        success: function (result) {
            $.each(result, function (i, data) {
                $('#MissionSkillList').append('<Option value=' + data.skillId + '>' + data.skillName + '</Option>');
            });
        }
    });
}




//$(document).ready(function () {

//    $('input[type=checkbox][id=countrylist]').change(function () {
//        var selectCountry = $('input[type=checkbox][id=countrylist]:checked').map(function () {
//            return $(this).val();
//        }).get();

//        if (selectCountry.length > 0) {
//            $('.card').hide();
//            $.each(selectCountry, function (index, value) {
//                $('.card:contains("' + value + '")').show();
//            });
//        } else {
//            $('.card').show();
//        }
//    });
//});


// sorting js goes here
document.getElementById("selectsort").addEventListener("change", function () {

    sortFunction(document.getElementById("selectsort").value);

});
function sortFunction(sortvalue) {
    let url = window.location.href;
    let seprator = url.indexOf('?') !== -1 ? '&' : '?';
    if (url.includes("sortby=")) {
        url = url.replace(/sortby=([^&]*)/, 'sortby=' + sortvalue);
    }
    else {
        url += seprator + 'sortby=' + sortvalue;
    }
    window.location.href = url;
}
// sorting js ends here


//search script goes here

//var debounceTimer;

//function debounce(func , delay) {
//    clearTimeout(debounceTimer);
//    debounceTimer = setTimeout(func , delay);
//}

//document.getElementById("myInput").addEventListener("input", function () {

//})

//search script ends here