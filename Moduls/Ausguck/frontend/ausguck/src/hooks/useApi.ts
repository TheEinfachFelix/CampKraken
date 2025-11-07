import axios from "axios";


function SubmitSurevey(surveyData: any) : Promise<boolean> {
    return axios.post("https://einfachfelix.ydns.eu/api/registration", surveyData).then(() => {
        return true;
    }).catch(() => {
        return false;
    });
}

export default SubmitSurevey;