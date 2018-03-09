## pd.appx install
* To know what pd.appx or pd.exe version to install: Press Win + Pause on your keyboard and should say x64-bassed or x86<br>
* If no Pause Break key on your keyboard: Press Win + X, Y
### Install pd.cer certificate:
* Download [pd.cer](https://github.com/dnaspider/PD/releases/download/v1.3.4/pd.cer "pd.appx certificate")
* Click pd.cer<br>
* Install Certificate…<br>
* Local Machine<br>
* Next<br>
* Yes<br>
* Place all certificates in the following store<br>
* Browse<br>
* Trusted Root Certification Authorities or Trusted People<br>
* Ok<br>
### Install pd.appx
* Download [pd.appx](https://github.com/dnaspider/PD/releases "pd.appx")<br>
* Install pd.cer if you haven't already<br>
* Press Win then type _For developers settings_ and enable Sideload apps or Developer mode<br>
* Make sure your Network adapter is not disabled<br>
* Click pd.appx to install<br>
### Delete installed pd.cer (optional)
* Win<br>
* Manage computer certificates<br>
* Trusted Root Certification Authorities<br>
* Certificates<br>
* Delete Issued To dnaspider@live.com<br>
### user.config (pd.appx)
* Manual backup: Make a copy user.config from C:\Users\..\AppData\Local\Packages\pd-_??\LocalCache\Local\pd\pd.exe_Url_??\1.3<br>
* Reset: close program, delete user.config then reopen program<br>
* Update: close program, edit user.config, save, then reopen program<br>
## pd.exe install
* Download [pd.exe](https://github.com/dnaspider/PD/releases/ "pd.exe") and run or
### Extract .exe from .appx
* Rename pd.appx to pd.appx.zip<br>
* Right click, Extract<br>
* pd.exe located in pd folder<br>
### pd.exe uninstall
* Delete pd.exe
* Delete pd folder from C:\Users\..\AppData\Local
### user.config (pd.exe)
* Reset: Close program then delete user.config from C:\Users\..\AppData\Local\pd\pd.exe_Url_??\1.3\ and reopen<br>
* Update: close program, edit user.config, save, then reopen program
* Manual backup: Make a copy user.config (right click hold, drag, release, copy here)