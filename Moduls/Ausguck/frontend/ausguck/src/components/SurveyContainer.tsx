import React, { useEffect, useState } from "react";
import { Model } from "survey-core";
import { Survey } from "survey-react-ui";
import "survey-core/survey-core.min.css";
import "../index.css";
import json from "./assets/survey.json";
import themeJson from "./assets/theme.json";
import SubmitSurevey from "../hooks/useApi";
import "survey-core/survey.i18n";
import { surveyLocalization, settings} from "survey-core";

surveyLocalization.defaultLocale = "de";

function SurveyComponent() {
    
    const [surveyModel, setSurveyModel] = useState(() => new Model(json));
    const [showDialog, setShowDialog] = useState(false);
    const [submitSuccess, setSubmitSuccess] = useState<boolean | null>(null);
    const [submitting, setSubmitting] = useState(false);

    useEffect(() => {
        const handler = async (sender: any/*, options*/) => {
            console.log("Survey results: ", sender.data);
            setSubmitting(true);
            try {
                const res = await SubmitSurevey(sender.data);
                console.log("SubmitSurevey response:", res);
                setSubmitSuccess(res);
            } catch (err) {
                console.error("SubmitSurevey Fehler:", err);
                setSubmitSuccess(false);
            } finally {
                setSubmitting(false);
                setShowDialog(true);
            }
        };

        surveyModel.onComplete.add(handler);
        return () => {
            surveyModel.onComplete.remove(handler);
        };
    }, [surveyModel]);

    const handleDialogOk = () => {
        setShowDialog(false);
        setSubmitSuccess(null);
        // Reset the survey to start from the beginning
        setSurveyModel(new Model(json));
    };

    surveyModel.applyTheme(themeJson);



    return (
        <>
            <div style={surveyContainerStyle}>
                <Survey model={surveyModel} />
            </div>

            {showDialog && (
                <div style={modalBackdropStyle} role="dialog" aria-modal="true">
                    <div style={modalStyle}>
                        <h3 style={{ marginTop: 0 }}>{submitSuccess ? "Erfolg" : "Fehler"}</h3>
                        <p>{submitting ? "Sende..." : (submitSuccess ? "Anmeldung erfolgreich übermittelt." : "Fehler beim Übermitteln der Anmeldung.\nBitte versuchen sie es erneut.")}</p>
                        <div style={{ display: "flex", justifyContent: "flex-end", gap: "8px" }}>
                            <button onClick={handleDialogOk} style={buttonStyle}>
                                OK
                            </button>
                        </div>
                    </div>
                </div>
            )}
        </>
    );
}

const modalBackdropStyle: React.CSSProperties = {
    position: "fixed",
    top: 0,
    left: 0,
    right: 0,
    bottom: 0,
    backgroundColor: "rgba(0,0,0,0.45)",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    zIndex: 9999,
};

const modalStyle: React.CSSProperties = {
    background: "#fff",
    padding: "20px",
    borderRadius: "8px",
    width: "90%",
    maxWidth: "420px",
    boxShadow: "0 10px 30px rgba(0,0,0,0.2)",
};

const buttonStyle: React.CSSProperties = {
    padding: "8px 14px",
    borderRadius: "6px",
    border: "none",
    background: "#1976d2",
    color: "white",
    cursor: "pointer",
};

export default SurveyComponent; 

const surveyContainerStyle: React.CSSProperties = {
};