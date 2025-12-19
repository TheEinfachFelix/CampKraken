import axios from "axios";
import ApiUrl from "./ApiUrl";

function SubmitSurevey(surveyData: any) : Promise<boolean> {
    return axios.post(ApiUrl + "/registration", surveyData).then(() => {
        return true;
    }).catch(() => {
        return false;
    });
}

export default SubmitSurevey;