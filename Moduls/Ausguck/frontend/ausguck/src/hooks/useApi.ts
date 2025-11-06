import axios from "axios";

const apiUrl = import.meta.env.VITE_API_URL as string;

function SubmitSurevey(surveyData: any) : Promise<boolean> {
    return axios.post(apiUrl, surveyData).then(() => {
        return true;
    }).catch(() => {
        return false;
    });
}

export default SubmitSurevey;