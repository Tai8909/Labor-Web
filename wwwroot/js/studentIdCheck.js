// don't know if the below code is still needed, I'll just keep it in the form comment
// function containSpecial(s) {
//     // Checks if the string contains special character
//     let containSpecial = RegExp(/[(\ )(\~)(\!)(\#)(\$)(\%)(\^)(\&)(\*)(\()(\))(\-)(\_)(\+)(\=)(\[)(\])(\{)(\})(\|)(\\)(\;)(\:)(\')(\")(\,)(\.)(\/)(\<)(\>)(\?)(\)]+/);
//     return (containSpecial.test(s));
// }
function checkidnt(e) {
    let studentID = document.getElementById('StudentID').value;
    let regexStudentID = /^([ABFMDLJKEWX](\d{7})|)$/;
    // let pattern2 = new RegExp("[A-Za-z]+");
    // if ((studentID.length != 8 && studentID.length != 10) || containSpecial(studentID) || pattern2.test(studentID.charAt(0)) == false) {
    if(!regexStudentID.test(studentID)){
        let errMsg = '格式輸入錯誤，請檢查有無非法字元或者其他問題';
        e.target.setCustomValidity(errMsg);
        document.getElementById("StudentID").setCustomValidity(errMsg);
        document.getElementById("StudentIDSmall").innerHTML = errMsg;
    }
    else {
        e.target.setCustomValidity('');
        document.getElementById("StudentID").setCustomValidity('');
        document.getElementById("StudentIDSmall").innerHTML = '';
    }
}