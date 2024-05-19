# 機能
## Google Cloud Storage
### Google Cloud Storage 上のテクスチャ（Texture2D）の取得
```cs
private async void Hoge()
{
    int width = 512;//取得する Texture2D の横幅
	
    int height = 512;//取得する Texture2D の縦幅

    Texture2D texture = await TSUBASAMUSU.Google.CloudStorage.CloudStorageObjectGetter.GetTextureFromCloudStorageAsync("JSON Web Token", "バケット名", "オブジェクト名", width, height);
}
```
### Google Cloud Storage 上のアセットバンドルからの任意の型のアセットの取得
```cs
private async void Hoge()
{
    {任意の型} asset = await TSUBASAMUSU.Google.CloudStorage.CloudStorageObjectGetter.GetAssetFromCloudStorage<{任意の型}>("JSON Web Token", "バケット名", "オブジェクト名", "アセット名");
}
```
## Google Cloud JSON Web Token
### Google Cloud の API を使用する際に必要な JSON Web Token の取得
```cs
private async void Hoge()
{
    string[] scopes = new string[1] {"必要な権限のURL"};

    string jwt = await TSUBASAMUSU.Google.JsonWebToken.GoogleCloudJwtGetter.GetGoogleCloudJwtAsync("サービスアカウントのプライベートキー", "サービスアカウントのメールアドレス", scopes);
}
```
## Google Spreadsheet
### Google Spreadsheet のセルへの文字列の設定
```cs
private void Hoge()
{
    int row = 1;//セルの行

    int column = 1;//セルの列

    TSUBASAMUSU.Google.Spreadsheet.SpreadsheetManager.SetCellValueAsync("JSON Web Token", "シートの ID", "シートの名前", row, column, "セルに設定する文字列");
}
```
### Google Spreadsheet のセルからの文字列の取得
```cs
private async void Hoge()
{
    (int row, int column) firstCell = (1, 1);//取得する範囲の最初のセル（行,列）
	
    (int row, int column) lastCell = (2, 2);//取得する範囲の最後のセル（行,列）

    List<List<string>> cellValues = await TSUBASAMUSU.Google.Spreadsheet.SpreadsheetManager.GetCellValuesAsync("JSON Web Token", "シートの ID", "シートの名前", firstCell, lastCell);

    string cellValue = cellValues[1][2];//1行目の2列目のセルの値
}
```
### Google Spreadsheet の指定の列の最終行番号の取得
```cs
private async void Hoge()
{
    int column = 1;//列

    int lastRow = await TSUBASAMUSU.Google.Spreadsheet.SpreadsheetManager.GetLastRowAsync("JSON Web Token", "シートの ID", "シートの名前", column);
}
```
## Unity Web Request
### ``UnityWebRequest.SendWebRequest()`` の ``await`` での待機
```cs
using TSUBASAMUSU.UnityWebRequestAwaiter;//名前空間を追加
using UnityEngine.Networking;

public class Sample
{
    private async void Hoge()
    {
        UnityWebRequest unityWebRequest = new UnityWebRequest();

        await unityWebRequest.SendWebRequest();
    }
}
```
# 使用方法
## 1. Unity の設定の変更
Unity エディタを開き、「**File** ＞ **Build Settings** ＞ **Player Settings...** ＞ **Player** ＞ **Other Settings** ＞ **Configuration** ＞ **Api Compatibility Level**」を「**.NET Standard 2.1**」に変更する。
## 2. NuGet パッケージのインポート
[NuGetForUnity](https://github.com/GlitchEnzo/NuGetForUnity) を使用してプロジェクトに以下の NuGet パッケージをインポートする。

- Newtonsoft.Json
## 3. DLLのインポート
最新版のリリースをダウンロードし、「**TsubasamusuUnityLibrary.dll**」をプロジェクトにインポートする。
