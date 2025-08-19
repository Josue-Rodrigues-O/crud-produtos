import { Injectable } from '@angular/core';
import { FieldValidation } from '../../models/field-validation';

@Injectable({
  providedIn: 'root'
})
export class RequestService {
  setErrorInInvalidFields(fields: FieldValidation[], errorsList: any) {
    let containsErrors = false;
    fields.forEach(field => {
      if ((errorsList[field.id] ?? []).length > 0) {
        containsErrors = true;
        field.field.setInvalidState(errorsList[field.id][0]);
      } else {
        field.field.setValidState();
      }
    });

    if (containsErrors)
      alert("Preencha os campos corretamente.")
  }
}
