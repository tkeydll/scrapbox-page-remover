# scrapbox-page-remover

Scrapbox からエクスポートしたファイルに対し、ページタイトルにフィルタをかけて、結果を json で保存します。

Scrapbox 自体はAPIによるページの一括削除をサポートしていないので、新しいプロジェクトを作成して不要なページを削除した json をインポートします。

## Usage

```bash
cd scrapbox-page-remover
dotnet run -s exported.json -o removed.json -p "<pattern>"
```
### Options

```bash
  -s, --source-file    Required. Set source file name (json).

  -o, --output         (Default: result.json) Set output file name.

  -p, --pattern        Required. Set the pattern to match the title to be deleted.

  -d, --debug          (Default: false) Show page title to debugging.

  --help               Display this help screen.

  --version            Display version information.
  ```
  