IPFS-GUI
==================

> Simple Windows UI providing IPFS integration

![](https://github.com/marcin212/ipfs-gui/blob/master/doc/img/gui.gif)

## Table of Contents

- [Features](#features)
- [Install](#install)
- [Build](#build)
- [License](#license)

## Features
* Manage daemon (start/stop/autostart)
* Add ipfs to path
* Add file to ipfs via the 'send to' menu
* Open webui
* More to come! If you have an idea leave an issue

## Install

* Build the things
  * Or get them at https://ipfs.io/ipfs/QmT7fjfKFhPMyXWFAugwEvoHxPtBdpScvhyuS9zFf6Wssp (v0.0.1) 
* Run the installer, you know what to do next!

## Build

### Base Program
* build ipfs-gui.sln with whatever is capable of doing it

### Installer
* First run `setup\preInnoSetup.ps1` to download ipfs build from `dist.ipfs.io`
* Build the rest using http://www.jrsoftware.org/isinfo.php

## License

MIT © marcin212
