<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="AquaAlert.Setting" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alarm Clock</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            text-align: center;
            background-color: #f2f2f2;
        }

        #clockContainer {
            width: 300px;
            margin: 50px auto;
            border: 2px solid #333;
            border-radius: 10px;
            padding: 20px;
            background-color: #fff;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        #timeDisplay {
            font-size: 24px;
            margin-bottom: 20px;
        }

        #alarmList {
            text-align: left;
            margin-bottom: 20px;
        }

        #alarmList li {
            list-style-type: none;
        }

        .btn {
            padding: 10px 20px;
            margin: 5px;
            background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .btn:hover {
            background-color: #0056b3;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="clockContainer">
            <div id="timeDisplay"></div>
            <ul id="alarmList"></ul>
            <div>
                <label for="alarmDateTime">Set Alarm (YYYY-MM-DD HH:MM AM/PM):</label>
                <input type="text" id="alarmDateTime" placeholder="Enter alarm date and time">
                <button type="button" class="btn" onclick="addAlarm()">Add Alarm</button>
                <button type="button" class="btn" onclick="addMultipleAlarms()">Add Multiple Alarms</button>
            </div>
        </div>
    </form>

    <script type="text/jscript">
        var alarms = [];

        function updateTime() {
            var now = new Date();
            var hours = now.getHours();
            var minutes = now.getMinutes();
            var seconds = now.getSeconds();

            hours = padZero(hours);
            minutes = padZero(minutes);
            seconds = padZero(seconds);

            document.getElementById('timeDisplay').innerText = hours + ":" + minutes + ":" + seconds;

            checkAlarms(now);
        }

        function padZero(num) {
            return (num < 10 ? '0' : '') + num;
        }

        function addAlarm() {
            var alarmDateTimeInput = document.getElementById('alarmDateTime').value;
            addAlarmToAlarms(alarmDateTimeInput);
        }

        function addMultipleAlarms() {
            var alarmTimesInput = prompt("Enter multiple alarm date and times (separated by commas, e.g., 2024-03-25 08:00 AM,2024-03-26 09:30 PM,2024-03-27 12:45 PM):");
            if (alarmTimesInput) {
                var alarmTimes = alarmTimesInput.split(',');
                alarmTimes.forEach(function (alarmDateTime) {
                    addAlarmToAlarms(alarmDateTime);
                });
            }
        }

        function addAlarmToAlarms(alarmDateTime) {
            var parts = alarmDateTime.split(' ');
            var dateParts = parts[0].split('-');
            var timeParts = parts[1].split(':');
            var ampm = parts[2].toUpperCase();
            var year = parseInt(dateParts[0]);
            var month = parseInt(dateParts[1]) - 1; // Month is zero-based
            var day = parseInt(dateParts[2]);
            var hours = parseInt(timeParts[0]);
            var minutes = parseInt(timeParts[1]);

            if (ampm === "PM" && hours < 12) {
                hours += 12;
            } else if (ampm === "AM" && hours === 12) {
                hours = 0;
            }

            var alarmDate = new Date(year, month, day, hours, minutes);
            var newAlarm = {
                datetime: alarmDate
            };
            alarms.push(newAlarm);
            renderAlarms();
        }

        function renderAlarms() {
            var alarmList = document.getElementById('alarmList');
            alarmList.innerHTML = '';
            alarms.forEach(function (alarm, index) {
                var listItem = document.createElement('li');
                var dateTimeString = alarm.datetime.toLocaleString();
                listItem.textContent = 'Alarm ' + (index + 1) + ': ' + dateTimeString;
                alarmList.appendChild(listItem);
            });
        }

        function checkAlarms(currentTime) {
            alarms.forEach(function (alarm, index) {
                if (currentTime.getTime() >= alarm.datetime.getTime()) {
                    playAlarm();
                    alarms.splice(index, 1); // Remove the triggered alarm
                    renderAlarms();
                }
            });
        }

        function playAlarm() {
            var audio = new Audio('alarm1.mp3');
            audio.play();
        }

        updateTime();
        setInterval(updateTime, 1000);
    </script>
</body>
</html>