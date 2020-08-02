# scrapbox-page-remover
Scrapbox からエクスポートしたファイルにフィルタをかけて、結果を json で保存します。
Scrapbox 自体はAPIによるページの一括削除をサポートしていないので、新しいプロジェクトを作成して不要なページを削除した json をインポートします。

## Usage

```bash
cd scrapbox-page-remover
dotnet run -s exported.json -o removed.json -p "<pattern>"
```
