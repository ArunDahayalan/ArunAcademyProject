$(document).ready(function () {
    setTimeout(function () {
        timeoutsubmitcall();
    }, 5400000);
});

var myVar = setInterval(function () {
    myTimer()
}, 60000);

var myVartwo = setInterval(function () {
    myTimertwo()
}, 1000);


var timeRemaining = 88;
var timeremainingseconds = 59;

function myTimertwo() {
    document.getElementById("timeseconds").innerHTML = timeremainingseconds--;
    if (timeremainingseconds == 0) {
        timeremainingseconds = 60;
    }
}

function myTimer() {
    
    document.getElementById("timeRemaining").innerHTML = timeRemaining--;
}


var currentTab = 0; // Current tab is set to be the first tab (0)
showTab(currentTab); // Display the crurrent tab

function showTab(n) {
  // This function will display the specified tab of the form...
  var x = document.getElementsByClassName("tab");
  x[n].style.display = "block";
  //... and fix the Previous/Next buttons:
  if (n == 0) {
    document.getElementById("prevBtn").style.display = "none";
  } else {
    document.getElementById("prevBtn").style.display = "inline";
  }
  if (n == (x.length - 1)) {
      document.getElementById("nextBtn").innerHTML = "Submit";
      $("#nextBtn").addClass("submitAnswers");
  } else {
      document.getElementById("nextBtn").innerHTML = "Next";
      $("#nextBtn").removeClass("submitAnswers");
  }
  //... and run a function that will display the correct step indicator:
  //fixStepIndicator(n)
}

function nextPrev(n) {
    if (n == 1 && $("#nextBtn").hasClass("submitAnswers"))
    {
        submitcall();
    }
    else {
        // This function will figure out which tab to display
        var x = document.getElementsByClassName("tab");
        // Exit the function if any field in the current tab is invalid:
        //if (n == 1 && !validateForm()) return false;
        // Hide the current tab:
        x[currentTab].style.display = "none";
        // Increase or decrease the current tab by 1:
        currentTab = currentTab + n;
        // if you have reached the end of the form...
        if (currentTab >= x.length) {
            // ... the form gets submitted:
            //document.getElementById("regForm").submit();
            return false;
        }
        // Otherwise, display the correct tab:
        showTab(currentTab);
    }

}

function timeoutsubmitcall()
{
    //var isAnsweredAll = true;
    //var notAnswered = [];
    var filledValues = [];
    var filledQuestions = [];
    $(".OptinalAnswers").each(function () {
        var checkedradio = false;
        ($(this).children("input")).each(function () {

            if ($(this).prop("checked")) {
                checkedradio = true;
                filledQuestions.push($(this).attr("name"));
                filledValues.push($(this).val());
            }

        });
        if (!checkedradio) {
            //isAnsweredAll = false;
            //notAnswered.push($(this).parent().attr("id"));
            filledQuestions.push($($(this).children("input")).attr("name"));
            filledValues.push(0);
        }
    });
        var data = {
            QuestionsList: filledQuestions,
            AnswersList: filledValues,
            QuestionGroupId: $(".questiongroupid").val()
        }
        $.ajax({
            type: "POST",
            url: "/testquestions",
            data: data,
            success: function (result) {
                if (result != null) {
                    window.location = "/testresult/" + $(".questiongroupid").val();
                }
            },

        });
}

function submitcall()
{
    var isAnsweredAll = true;
    var notAnswered = [];
    var filledValues = [];
    var filledQuestions = [];
    $(".OptinalAnswers").each(function () {
        var checkedradio = false;
        ($(this).children("input")).each(function () {
            
            if($(this).prop("checked"))
            {
                checkedradio = true;
                filledQuestions.push($(this).attr("name"));
                filledValues.push($(this).val());
            }

        });
        if (!checkedradio) {
            isAnsweredAll = false;
            filledQuestions.push($(this).attr("name"));
            filledValues.push(0);
            notAnswered.push($(this).parent().attr("id"));
        }
    });
    if (isAnsweredAll) {
        var data ={
            QuestionsList : filledQuestions,
            AnswersList: filledValues,
            QuestionGroupId: $(".questiongroupid").val()
        }
        $.ajax({
                    type: "POST",
                    url: "/testquestions",
                    data: data,
                    success: function (result) {
                        if(result != null)
                        {
                            window.location = "/testresult/" + $(".questiongroupid").val();
                        }
                    },

       });

    }
    else {
        //alert("Please fill " + notAnswered.length + " answer(s)");
        if (confirm("Not filled " + notAnswered.length + " answers. Click ok to continue"))
        {
            var data = {
                QuestionsList: filledQuestions,
                AnswersList: filledValues,
                QuestionGroupId: $(".questiongroupid").val()
            }
            $.ajax({
                type: "POST",
                url: "/testquestions",
                data: data,
                success: function (result) {
                    if (result != null) {
                        window.location = "/testresult/" + $(".questiongroupid").val();
                    }
                },

            });
        }
        else {

        }
    }
}