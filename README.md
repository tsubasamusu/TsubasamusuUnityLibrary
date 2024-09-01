# 機能
## Google Cloud Storage
### Google Cloud Storage 上のテクスチャを Texture2D として取得する
```cs
private async void Hoge()
{
    int width = 512;//取得する Texture2D の横幅
	
    int height = 512;//取得する Texture2D の縦幅

    Texture2D texture = await TSUBASAMUSU.Google.CloudStorage.CloudStorageObjectGetter.GetTextureFromCloudStorageAsync("JSON Web Token", "バケット名", "オブジェクト名", width, height);
}
```
### Google Cloud Storage 上のアセットバンドルから任意の型のアセットを取得する
```cs
private async void Hoge()
{
    //取得するアセットバンドルの含んでいるアセットが1つのみの場合
    (任意の型, AssetBundle) asset = await TSUBASAMUSU.Google.CloudStorage.CloudStorageObjectGetter.GetAssetFromCloudStorageAsync<{任意の型}>("JSON Web Token", "バケット名", "オブジェクト名", "アセット名");

    //取得するアセットバンドルの含んでいるアセットが複数ある場合
    (任意の型の配列, AssetBundle) assets = await TSUBASAMUSU.Google.CloudStorage.CloudStorageObjectGetter.GetAllAssetsFromCloudStorageAsync<{任意の型}>("JSON Web Token", "バケット名", "オブジェクト名");
}
```
## Google Cloud JSON Web Token
### Google Cloud の API を使用する際に必要な JSON Web Token を取得する
```cs
private async void Hoge()
{
    string[] scopes = new string[1] {"必要な権限のURL"};

    string jwt = await TSUBASAMUSU.Google.JsonWebToken.GoogleCloudJwtGetter.GetGoogleCloudJwtAsync("サービスアカウントのプライベートキー", "サービスアカウントのメールアドレス", scopes);
}
```
## Google Spreadsheet
### Google Spreadsheet の任意のセルへ文字列を設定する
```cs
private void Hoge()
{
    int row = 1;//セルの行

    int column = 1;//セルの列

    TSUBASAMUSU.Google.Spreadsheet.SpreadsheetManager.SetCellValueAsync("JSON Web Token", "シートの ID", "シートの名前", row, column, "セルに設定する文字列");
}
```
### Google Spreadsheet の任意のセルから文字列を取得する
```cs
private async void Hoge()
{
    (int row, int column) firstCell = (1, 1);//取得する範囲の最初のセル（行,列）
	
    (int row, int column) lastCell = (2, 2);//取得する範囲の最後のセル（行,列）

    List<List<string>> cellValues = await TSUBASAMUSU.Google.Spreadsheet.SpreadsheetManager.GetCellValuesAsync("JSON Web Token", "シートの ID", "シートの名前", firstCell, lastCell);

    string cellValue = cellValues[1][2];//1行目の2列目のセルの値
}
```
### Google Spreadsheet の指定の列の最終行の番号を取得する
```cs
private async void Hoge()
{
    int column = 1;//列

    int lastRow = await TSUBASAMUSU.Google.Spreadsheet.SpreadsheetManager.GetLastRowAsync("JSON Web Token", "シートの ID", "シートの名前", column);
}
```
## ライティング
### 任意のライトマップ配列を現在のシーンに適用する
```cs
private void Hoge()
{
    TSUBASAMUSU.Lighting.LightingUtility.ApplyLightmapsToCurrentScene( Texture2D 型のカラーライトマップの配列, Texture2D 型の法線ライトマップの配列);
}
```
### ライトマップのリストを番号順に並び替える
```cs
private void Hoge()
{
    bool success = TSUBASAMUSU.Lighting.LightingUtility.SortLightmaps(ref Texture2D 型のライトマップのリスト);
}
```
### MeshRenderer 用のライトマップのデータを作成する
```cs
[MenuItem("Assets/Create/Lightmap Data JSON File")]
public static async void Hoge()
{
    //現在開いているシーン内の MeshRenderer のライトマップの設定を JSON ファイルに出力
    bool success = await TSUBASAMUSU.Lighting.LightingUtility.CreateJsonFileForLightingAsync();
}
```
### MeshRenderer 用のライトマップのデータを現在のシーンに適用する
```cs
private void Hoge()
{
    // CreateJsonFileForLightingAsync() で出力した JSON ファイルのデータを現在のシーンに適用
    bool success = TSUBASAMUSU.Lighting.LightingUtility.ApplyLightmapsToMeshRenderers(ライトマップのデータの JSON ファイル);
}
```
## その他
### ``UnityWebRequest.SendWebRequest()`` を ``await`` で待機する
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
### Assets フォルダ直下へ JSON ファイルを作成する
```cs
private async void Hoge()
{
    await TSUBASAMUSU.UnityEditor.AssetUtility.CreateJsonFileAtRootDirectoryAsync("JSON 形式のテキスト", "ファイル名");
}
```
### 現在の URL からクエリパラメーターを取得する（WebGL 用）
```cs
private void Hoge()
{
    //key,value
    Dictionary<string, string> queryParameters = TSUBASAMUSU.Other.WebUtility.GetQueryParameters();
}
```
# 使用方法
## 1. Unity の設定の変更
Unity エディタを開き、「**File** ＞ **Build Settings** ＞ **Player Settings...** ＞ **Player** ＞ **Other Settings** ＞ **Configuration** ＞ **Api Compatibility Level**」を「**.NET Standard 2.1**」に変更する。
## 2. NuGet パッケージのインポート
[NuGetForUnity](https://github.com/GlitchEnzo/NuGetForUnity) を使用してプロジェクトに以下の NuGet パッケージをインポートする。

- Newtonsoft.Json
## 3. DLL のインポート
最新版のリリースをダウンロードして「**TsubasamusuUnityLibrary.dll**」をプロジェクトにインポートする。
