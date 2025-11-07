import { Model } from "survey-core";
import { Survey } from "survey-react-ui";
import "survey-core/survey-core.min.css";
import "../index.css";
import json from "./assets/survey.json";
import SubmitSurevey from "../hooks/useApi";
import "survey-core/survey.i18n";
import { surveyLocalization } from "survey-core";

surveyLocalization.defaultLocale = "de";

function SurveyComponent() {
    const survey = new Model(json);
    survey.onComplete.add((sender/*, options*/) => {
        var response = SubmitSurevey(sender.data);
        console.log("Survey results: ");
        console.log(JSON.stringify(sender.data, null, 3));
        console.log("SubmitSurevey response: ");
        response.then((res) => { console.log(res);});
        
    });
    return (<Survey model={survey} />);
}

export default SurveyComponent;