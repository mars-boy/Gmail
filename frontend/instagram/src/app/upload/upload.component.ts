import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { retry } from 'rxjs/operators';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {


  selectedFiles: File[];


  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
  }

  fileChange(element){
    this.selectedFiles = element.target.files;
  }

  async Submit(){
    for(var file of this.selectedFiles){
      var fileSize = file.size, lastByte=0, c=0,chunckSize=10224*1024;
      var start=lastByte, end=0, chunckId=0, totalChuncks=0;
      if(file.size%chunckSize == 0){
        totalChuncks = file.size/chunckSize;
      }else{
        totalChuncks = Math.floor(file.size/chunckSize)+1;
      }
      for(;lastByte<fileSize;c++){
        debugger;
        chunckId += 1;
        start=lastByte;
        end = ((lastByte+chunckSize)<(fileSize-1)) ? (lastByte+chunckSize): fileSize;
        var blob = file.slice(start, end);
        lastByte+=chunckSize;
        let headers = new HttpHeaders();
        let fileName = file.name;
        headers = headers.set('fileName', fileName);
        headers = headers.set('TotalChuncks', totalChuncks+'');
        headers = headers.set('CurrentChunck', chunckId+'');
        let object = {
          'FileName': fileName
        }
        var formData = new FormData();
        formData.append('parameters', JSON.stringify(object));
        formData.append('file', blob);
        await this.formUpload(headers, formData);
      }
    }
  }

  async formUpload(headers: HttpHeaders, formData: FormData){
    debugger;
    await this.httpClient.post('http://localhost:5001/api/Image/Upload', formData, { headers }).pipe(
      retry(3)
    ).toPromise();
  }
}
