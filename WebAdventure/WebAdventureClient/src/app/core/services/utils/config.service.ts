import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Headers, RequestOptions } from '@angular/http';

@Injectable()
export class ConfigService {

   private _apiURI: string;

    constructor() {
       this._apiURI = 'https://localhost:44337/api/';
    }

    getHeaders() {
        return new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');
    }

    getApiURI() {
        return this._apiURI;
    }
}
