# coding=GBK
import unittest
from pywinauto import application
from pywinauto import timings
import time
import config
import pymssql
import serial
import json

class CJTest(unittest.TestCase):
    "测试采集功能"

    ##初始化工作
    def setUp(self):
        pass

    #退出清理工作
    def tearDown(self):
        pass

    def step1(self):
        print 'test cai ji'
        app = application.Application()
        app.start_(config.app_cj_path, timeout=20, retry_interval=.5)
        window = timings.WaitUntilPasses(20, .5, lambda: app.window_(title_re=u'.*用户登录.*'))
        window.Wait('ready', 20, .5)
        window.Edit2.TypeKeys(config.app_user_name)
        window.Edit1.TypeKeys(config.app_password)
        time.sleep(2)
        timings.WaitUntil(30, 0.5, window.Button1.IsEnabled, True)
        window.Button2.Click()

        win1 = timings.WaitUntilPasses(20, .5, lambda: app.window_(title_re=u'.*选取就绪试验编号.*'))
        win1.Wait('ready', 100, .5)
        #print win1.TreeView.Texts()

        #print win1.TreeView.Root()
        node = win1.TreeView2.GetItem(path=u'\ \待做试验项目\20140729151356' )
        rct = node.Rectangle()
        win1.SysTreeView32.ClickInput(coords=(48, 47), wheel_dist=.5)
        print rct.left - 5, rct.top + 5
        win1.SysTreeView32.ClickInput(coords=(rct.left - 5, rct.top + 5), wheel_dist=.5)
        time.sleep(3)
        win1.Button.Click()
        mainform = timings.WaitUntilPasses(30, .5, lambda: app.window_(title_re=u'.*铁路试验实时采集系统.*'))
        mainform.Wait('ready', 20, 1)
        time.sleep(2)
        try:
            mainform.Button1.Click()
        except:
            print "error get "
        print 'start wait ' + time.strftime("%Y%m%d%H%M%S", time.localtime())

        timings.WaitUntil(20, 0.5, mainform.Button1.IsEnabled, True)
        print 'end wait ' + time.strftime("%Y%m%d%H%M%S", time.localtime())

    def step2(self):
        conn = pymssql.connect(host=config.host[2], user='sygldb_kingrocket_f ', password='wdxlzyn@#830',
                                   database='SYGLDB_ZhengXu', charset="utf8")
        cur = conn.cursor()
        if not cur:
            raise (NameError, "连接数据库失败")
        else:
            print 'connected'

        sql = "SELECT RealTimeData FROM SYGLDB_ZhengXu.dbo.sys_test_data WHERE ModuleID='68F05EBC-5D34-49C5-9B57-49B688DF24F7' AND wtbh='ZXZQ01-02-GJ-W2014062208'ORDER BY SerialNumber'"

        cur.execute(sql)
        lines = cur.fetchall()
        #f = open("e:\\1.txt")
        t = serial.Serial("com3", 9600)
        for line in lines:
            items = json.loads(line)
            #print items
            i = 0
            j = len(items)
            #print j
            while i < j:

                if items[i]["Value"] is None:
                    print "none"
                    pass
                else:
                    strInput = repr(items[i]["Value"])
                    print strInput
                n = t.write('a' + strInput + 'b')
                #t.read(n)
                i += 1
                time.sleep(0.1)
        t.close()
        f.close()
        timings.WaitUntil(10, 0.5, self.mainform.Button11.IsEnabled, True)
        time.sleep(5)
        self.app.kill_()

    def step3(self):
        def GetCellValue(doc, sheetID, cellName):
            """
            :param sheetID:
            :param cellName:
            :return:
            """
            if doc is not None:
                for sheet in doc["Sheets"]:
                    dump = json.dumps(sheet)
                    sheet = json.loads(dump)

                    if sheetID in sheet["ID"]:
                        dump = json.dumps(sheet["Cells"])
                        cell = json.loads(dump)
                        i = 0
                        j = len(cell)
                        while i < j:
                            if cellName in cell[i]["Name"]:
                                return cell[i]["Value"]
                            i += 1

        conn = pymssql.connect(host='112.124.99.146', user='sygldb_kingrocket_f ', password='wdxlzyn@#830',
                               database='SYGLDB_Demo', charset="utf8")

        cur = conn.cursor()
        if not cur:
            raise (NameError, "连接数据库失败")
        else:
            print 'connected'

        sql = "SELECT data  FROM dbo.sys_document WHERE WTBH ='" + self.wtbh + "'"
        cur.execute(sql)
        resList = cur.fetchall()
        dump = json.dumps(resList)
        item = json.loads(dump)
        str = item[0][0]
        items = json.loads(str)
        value = GetCellValue(doc=items, sheetID='270a1da6-2045-405a-ae77-18c0c98c1edd', cellName='D29')
        if value is not None:
            print value
        else:
            print "error get "


        #assert(value == 549.66), " find value wrong!"

        cur.close()
        conn.close()
        print 'closed'


    def CJTest(self):
        self.step1()
        self.step2()
        self.step3()



if __name__ == '__main__':
    unittest.main()
