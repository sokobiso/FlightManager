$(function () {
    $("#updateFlight").hide();
    $("#saveSucsses").hide();
    $("#saveFailed").hide();
    LoadFlights();
    $("#submitFlight").click(function () {
        $("#hdIdFlight").val(0);
        SaveOrUpdate();
    });

    $("#updateFlight").click(function () {
        SaveOrUpdate();
    });

    function SaveOrUpdate() {
        $("#saveSucsses").hide();
        $("#saveFailed").hide();

        var data = $("#flightForm").serialize();
        //Set the URL.
        var url = $("#flightForm").attr("action");

        $.ajax({
            method: 'POST',
            url: url,
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
            data: data,
            success: function (result) {
                LoadFlights();
                $("#updateFlight").hide();
                $("#saveSucsses").show();
            },
            error: function () {
                $("#saveFailed").show();
                console.log('Failed ');
            }
        });
    }

    $("#ddlDepartureAirportid").change(function () {
        flightInfo();
    });
    $("#ddlArrivalAirportId").change(function () {
        flightInfo();
    });

  
    function flightInfo() {

        var url = "FlightDistance";
        var airPortDepartureId = $('#ddlDepartureAirportid').val();
        var airPortArrivalId = $('#ddlArrivalAirportId').val();
        var data = { airPortDepartureId: airPortDepartureId, airPortArrivalId: airPortArrivalId };

        $.ajax({
            method: 'GET',
            url: url,
            data: data,
            success: function (result) {
                $("#lblDistnace").text(result);
            },
            error: function () {
                
                console.log('Failed ');
            }
        });

    }

    $('body').on('click', '.item-action-delete', function () {
        $("#updateFlight").hide();
        $("#saveSucsses").hide();
        $("#saveFailed").hide();
        var url = "delete";
        var id = $(this).data('id');
        var data = {
            id: id
        };
        $.ajax({
            method: 'DELETE',
            url: url,
            data: data,
            success: function (result) {
                $("#saveSucsses").show();  
                LoadFlights();
            },
            error: function () {
                $("#saveFailed").show();  
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });

    });

    $('body').on('click', '.item-action-edit', function () {
        $("#saveSucsses").hide();
        $("#saveFailed").hide();
        var url = "edit";
        var id = $(this).data('id');
        var data = {
            id: id
        };
        $.ajax({
            method: 'GET',
            url: url,
            data: data,
            success: function (result) {
                $("#txtName").val(result.name);

                $("#ddlDepartureAirportid").val(result.selectedDepartureAirportid);
                $("#ddlArrivalAirportId").val(result.selectedArrivalAirportId);
                $("#hdIdFlight").val(result.id);
                $("#updateFlight").show();
                flightInfo();
            },
            error: function () {
           
                console.log('Failed ');
            }
        });

    });

    function LoadFlights() {
        var url = "_listFlights";
        $.ajax({
            method: 'GET',
            url: url,
            success: function (result) {
                $("#FlightsListe").html(result);
            },
            error: function () {
            }
        });
    }
});