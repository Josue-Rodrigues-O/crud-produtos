import { Injectable } from '@angular/core';
import { Department } from '../../models/department';
import { HttpClient } from '@angular/common/http';
import { SelectItem } from '../../models/select-item';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  private url = '/api/departments';

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<Department[]>(this.url).pipe(
      map(deps => deps.map<SelectItem>(dep => ({
        key: dep.codigo,
        description: dep.descricao
      }))));
  }

  getByCode(code: string) {
    let url = `${this.url}/${code}`;
    return this.http.get<Department>(url);
  }
}
