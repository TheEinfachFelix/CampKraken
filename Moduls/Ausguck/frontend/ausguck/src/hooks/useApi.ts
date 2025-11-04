import axios from "axios";

function SubmitSurevey(surveyData: any) : Promise<boolean> {
    return axios.post('localhost:32769/api/registration', surveyData).then(() => {
        return true;
    }).catch(() => {
        return false;
    });
}

export default SubmitSurevey;