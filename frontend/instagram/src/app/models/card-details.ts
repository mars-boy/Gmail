export class CardDetails {

    constructor(userName: string, postUrl: string, likeCount: Number ){
        this.userName = userName;
        this.postUrl = postUrl;
        this.likeCount = likeCount;
    }

    public userName : string;
    public postUrl : string;
    public likeCount : Number;
}
