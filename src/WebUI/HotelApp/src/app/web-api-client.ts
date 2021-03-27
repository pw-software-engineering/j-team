/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.9.4.0 (NJsonSchema v10.3.1.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

export interface ITodoItemsClient {
  getTodoItemsWithPagination(
    listId: number | undefined,
    pageNumber: number | undefined,
    pageSize: number | undefined
  ): Observable<PaginatedListOfHotelDto>;
  create(command: CreateHotelCmd): Observable<number>;
  update(id: number, command: UpdateHotelCmd): Observable<FileResponse>;
  delete(id: number): Observable<FileResponse>;
}

@Injectable({
  providedIn: 'root',
})
export class TodoItemsClient implements ITodoItemsClient {
  private http: HttpClient;
  private baseUrl: string;
  protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

  constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
    this.http = http;
    this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : '';
  }

  getTodoItemsWithPagination(
    listId: number | undefined,
    pageNumber: number | undefined,
    pageSize: number | undefined
  ): Observable<PaginatedListOfHotelDto> {
    let url_ = this.baseUrl + '/api/TodoItems?';
    if (listId === null) throw new Error("The parameter 'listId' cannot be null.");
    else if (listId !== undefined) url_ += 'ListId=' + encodeURIComponent('' + listId) + '&';
    if (pageNumber === null) throw new Error("The parameter 'pageNumber' cannot be null.");
    else if (pageNumber !== undefined) url_ += 'PageNumber=' + encodeURIComponent('' + pageNumber) + '&';
    if (pageSize === null) throw new Error("The parameter 'pageSize' cannot be null.");
    else if (pageSize !== undefined) url_ += 'PageSize=' + encodeURIComponent('' + pageSize) + '&';
    url_ = url_.replace(/[?&]$/, '');

    let options_: any = {
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({
        Accept: 'application/json',
      }),
    };

    return this.http
      .request('get', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processGetTodoItemsWithPagination(response_);
        })
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processGetTodoItemsWithPagination(<any>response_);
            } catch (e) {
              return <Observable<PaginatedListOfHotelDto>>(<any>_observableThrow(e));
            }
          } else return <Observable<PaginatedListOfHotelDto>>(<any>_observableThrow(response_));
        })
      );
  }

  protected processGetTodoItemsWithPagination(response: HttpResponseBase): Observable<PaginatedListOfHotelDto> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse
        ? response.body
        : (<any>response).error instanceof Blob
        ? (<any>response).error
        : undefined;

    let _headers: any = {};
    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }
    if (status === 200) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText) => {
          let result200: any = null;
          let resultData200 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
          result200 = PaginatedListOfHotelDto.fromJS(resultData200);
          return _observableOf(result200);
        })
      );
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText) => {
          return throwException('An unexpected server error occurred.', status, _responseText, _headers);
        })
      );
    }
    return _observableOf<PaginatedListOfHotelDto>(<any>null);
  }

  create(command: CreateHotelCmd): Observable<number> {
    let url_ = this.baseUrl + '/api/TodoItems';
    url_ = url_.replace(/[?&]$/, '');

    const content_ = JSON.stringify(command);

    let options_: any = {
      body: content_,
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Accept: 'application/json',
      }),
    };

    return this.http
      .request('post', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processCreate(response_);
        })
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processCreate(<any>response_);
            } catch (e) {
              return <Observable<number>>(<any>_observableThrow(e));
            }
          } else return <Observable<number>>(<any>_observableThrow(response_));
        })
      );
  }

  protected processCreate(response: HttpResponseBase): Observable<number> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse
        ? response.body
        : (<any>response).error instanceof Blob
        ? (<any>response).error
        : undefined;

    let _headers: any = {};
    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }
    if (status === 200) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText) => {
          let result200: any = null;
          let resultData200 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
          result200 = resultData200 !== undefined ? resultData200 : <any>null;
          return _observableOf(result200);
        })
      );
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText) => {
          return throwException('An unexpected server error occurred.', status, _responseText, _headers);
        })
      );
    }
    return _observableOf<number>(<any>null);
  }

  update(id: number, command: UpdateHotelCmd): Observable<FileResponse> {
    let url_ = this.baseUrl + '/api/TodoItems/{id}';
    if (id === undefined || id === null) throw new Error("The parameter 'id' must be defined.");
    url_ = url_.replace('{id}', encodeURIComponent('' + id));
    url_ = url_.replace(/[?&]$/, '');

    const content_ = JSON.stringify(command);

    let options_: any = {
      body: content_,
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Accept: 'application/octet-stream',
      }),
    };

    return this.http
      .request('put', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processUpdate(response_);
        })
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processUpdate(<any>response_);
            } catch (e) {
              return <Observable<FileResponse>>(<any>_observableThrow(e));
            }
          } else return <Observable<FileResponse>>(<any>_observableThrow(response_));
        })
      );
  }

  protected processUpdate(response: HttpResponseBase): Observable<FileResponse> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse
        ? response.body
        : (<any>response).error instanceof Blob
        ? (<any>response).error
        : undefined;

    let _headers: any = {};
    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }
    if (status === 200 || status === 206) {
      const contentDisposition = response.headers ? response.headers.get('content-disposition') : undefined;
      const fileNameMatch = contentDisposition ? /filename="?([^"]*?)"?(;|$)/g.exec(contentDisposition) : undefined;
      const fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[1] : undefined;
      return _observableOf({ fileName: fileName, data: <any>responseBlob, status: status, headers: _headers });
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText) => {
          return throwException('An unexpected server error occurred.', status, _responseText, _headers);
        })
      );
    }
    return _observableOf<FileResponse>(<any>null);
  }

  delete(id: number): Observable<FileResponse> {
    let url_ = this.baseUrl + '/api/TodoItems/{id}';
    if (id === undefined || id === null) throw new Error("The parameter 'id' must be defined.");
    url_ = url_.replace('{id}', encodeURIComponent('' + id));
    url_ = url_.replace(/[?&]$/, '');

    let options_: any = {
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({
        Accept: 'application/octet-stream',
      }),
    };

    return this.http
      .request('delete', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processDelete(response_);
        })
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processDelete(<any>response_);
            } catch (e) {
              return <Observable<FileResponse>>(<any>_observableThrow(e));
            }
          } else return <Observable<FileResponse>>(<any>_observableThrow(response_));
        })
      );
  }

  protected processDelete(response: HttpResponseBase): Observable<FileResponse> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse
        ? response.body
        : (<any>response).error instanceof Blob
        ? (<any>response).error
        : undefined;

    let _headers: any = {};
    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }
    if (status === 200 || status === 206) {
      const contentDisposition = response.headers ? response.headers.get('content-disposition') : undefined;
      const fileNameMatch = contentDisposition ? /filename="?([^"]*?)"?(;|$)/g.exec(contentDisposition) : undefined;
      const fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[1] : undefined;
      return _observableOf({ fileName: fileName, data: <any>responseBlob, status: status, headers: _headers });
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText) => {
          return throwException('An unexpected server error occurred.', status, _responseText, _headers);
        })
      );
    }
    return _observableOf<FileResponse>(<any>null);
  }
}

export class PaginatedListOfHotelDto implements IPaginatedListOfHotelDto {
  items?: HotelDto[] | undefined;
  pageIndex?: number;
  totalPages?: number;
  totalCount?: number;
  hasPreviousPage?: boolean;
  hasNextPage?: boolean;

  constructor(data?: IPaginatedListOfHotelDto) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property)) (<any>this)[property] = (<any>data)[property];
      }
    }
  }

  init(_data?: any) {
    if (_data) {
      if (Array.isArray(_data['items'])) {
        this.items = [] as any;
        for (let item of _data['items']) this.items!.push(HotelDto.fromJS(item));
      }
      this.pageIndex = _data['pageIndex'];
      this.totalPages = _data['totalPages'];
      this.totalCount = _data['totalCount'];
      this.hasPreviousPage = _data['hasPreviousPage'];
      this.hasNextPage = _data['hasNextPage'];
    }
  }

  static fromJS(data: any): PaginatedListOfHotelDto {
    data = typeof data === 'object' ? data : {};
    let result = new PaginatedListOfHotelDto();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    if (Array.isArray(this.items)) {
      data['items'] = [];
      for (let item of this.items) data['items'].push(item.toJSON());
    }
    data['pageIndex'] = this.pageIndex;
    data['totalPages'] = this.totalPages;
    data['totalCount'] = this.totalCount;
    data['hasPreviousPage'] = this.hasPreviousPage;
    data['hasNextPage'] = this.hasNextPage;
    return data;
  }
}

export interface IPaginatedListOfHotelDto {
  items?: HotelDto[] | undefined;
  pageIndex?: number;
  totalPages?: number;
  totalCount?: number;
  hasPreviousPage?: boolean;
  hasNextPage?: boolean;
}

export class HotelDto implements IHotelDto {
  id?: number;
  title?: string | undefined;

  constructor(data?: IHotelDto) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property)) (<any>this)[property] = (<any>data)[property];
      }
    }
  }

  init(_data?: any) {
    if (_data) {
      this.id = _data['id'];
      this.title = _data['title'];
    }
  }

  static fromJS(data: any): HotelDto {
    data = typeof data === 'object' ? data : {};
    let result = new HotelDto();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data['id'] = this.id;
    data['title'] = this.title;
    return data;
  }
}

export interface IHotelDto {
  id?: number;
  title?: string | undefined;
}

export class CreateHotelCmd implements ICreateHotelCmd {
  listId?: number;
  title?: string | undefined;

  constructor(data?: ICreateHotelCmd) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property)) (<any>this)[property] = (<any>data)[property];
      }
    }
  }

  init(_data?: any) {
    if (_data) {
      this.listId = _data['listId'];
      this.title = _data['title'];
    }
  }

  static fromJS(data: any): CreateHotelCmd {
    data = typeof data === 'object' ? data : {};
    let result = new CreateHotelCmd();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data['listId'] = this.listId;
    data['title'] = this.title;
    return data;
  }
}

export interface ICreateHotelCmd {
  listId?: number;
  title?: string | undefined;
}

export class UpdateHotelCmd implements IUpdateHotelCmd {
  id?: number;
  title?: string | undefined;
  done?: boolean;

  constructor(data?: IUpdateHotelCmd) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property)) (<any>this)[property] = (<any>data)[property];
      }
    }
  }

  init(_data?: any) {
    if (_data) {
      this.id = _data['id'];
      this.title = _data['title'];
      this.done = _data['done'];
    }
  }

  static fromJS(data: any): UpdateHotelCmd {
    data = typeof data === 'object' ? data : {};
    let result = new UpdateHotelCmd();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data['id'] = this.id;
    data['title'] = this.title;
    data['done'] = this.done;
    return data;
  }
}

export interface IUpdateHotelCmd {
  id?: number;
  title?: string | undefined;
  done?: boolean;
}

export interface FileResponse {
  data: Blob;
  status: number;
  fileName?: string;
  headers?: { [name: string]: any };
}

export class SwaggerException extends Error {
  message: string;
  status: number;
  response: string;
  headers: { [key: string]: any };
  result: any;

  constructor(message: string, status: number, response: string, headers: { [key: string]: any }, result: any) {
    super();

    this.message = message;
    this.status = status;
    this.response = response;
    this.headers = headers;
    this.result = result;
  }

  protected isSwaggerException = true;

  static isSwaggerException(obj: any): obj is SwaggerException {
    return obj.isSwaggerException === true;
  }
}

function throwException(
  message: string,
  status: number,
  response: string,
  headers: { [key: string]: any },
  result?: any
): Observable<any> {
  if (result !== null && result !== undefined) return _observableThrow(result);
  else return _observableThrow(new SwaggerException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
  return new Observable<string>((observer: any) => {
    if (!blob) {
      observer.next('');
      observer.complete();
    } else {
      let reader = new FileReader();
      reader.onload = (event) => {
        observer.next((<any>event.target).result);
        observer.complete();
      };
      reader.readAsText(blob);
    }
  });
}