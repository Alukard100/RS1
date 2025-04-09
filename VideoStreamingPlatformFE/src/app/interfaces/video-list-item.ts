export interface VideoListItem {
    VideoId: number;
    VideoName: string;
    CategoryId: number;
    UserName: string;
    ClickCounter: number;
    ThumbnailPicture: Base64URLString;
    UploadDate: Date;
    DurationInSeconds: number;
    CategoryName: string;
}
