#coding=GBK
__author__ = 'Administrator'

import shutil


def moveFileto():
    url = 'C:\workspace\workspace\铁路试验信息管理系统\RefrenceCenter\\'
    file = ['铁路试验实时采集系统.exe', 'ShuXianCaiJiComponents.dll', 'ShuXianCaiJiHlperClient.dll','ShuXianCaiJiModule.dll']
    tar1 = 'C:\workspace\Debug_WN'
    tar2 = 'C:\workspace\Debug_YL'
    i = 0
    j = len(file)
    while i < j:
        sor = url + file[i]
        shutil.copy(sor, tar1)
        shutil.copy(sor, tar2)
        i += 1

if __name__ == "__main__":
    moveFileto()
