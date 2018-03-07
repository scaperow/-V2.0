# coding=GBK
import unittest
from pywinauto import application
from pywinauto import timings
from pywinauto.win32functions import *
import time
import config
import xml_helper
import datetime
import common_help



class SystemTest(unittest.TestCase):
    def setUp(self):

        timings.Timings.Defaults()
        self.case_list = xml_helper.get_test_cases()
        self.app = application.Application()
        pass

    def tearDown(self):
        pass

    def step1(self):
        self.app.start_(config.app_gl_path, timeout=20, retry_interval=.5)
        login_window = timings.WaitUntilPasses(120, .5, lambda: self.app.window_(title_re=u'.*用户登录.*'))
        login_window.Wait('ready', 20, 1)

        login_window.Edit1.TypeKeys(config.app_user_name)
        login_window.Edit2.TypeKeys(config.app_password)
        timings.WaitUntil(20, 0.5, login_window.Button1.IsEnabled, True)
        time.sleep(1)
        login_window.Button1.Click()

        self.main_form = timings.WaitUntilPasses(30, .5, lambda: self.app.window_(title_re=u'.*铁路试验信息管理系统.*'))
        time.sleep(2)
        self.main_form.Wait('ready', 120, 1)
        time.sleep(3)
        self.main_form.GetFocus()
        BringWindowToTop(self.main_form.handle)

    def step2(self, case):
        self.main_form.PressMouseInput(coords=(11, 115))
        self.main_form.MoveMouse(coords=(11, 117))
        self.main_form.Main_TabControl.ClickInput(coords=(100, 45), double=True, wheel_dist=.5)

        win1 = timings.WaitUntilPasses(20, .5, lambda: self.app.window_(title_re=u'.*待做事项列表.*'))
        win1.Wait('ready', 60, 1)
        BringWindowToTop(win1.handle)
        win1.Close()
        self.main_form.PressMouseInput(coords=(703, 334))
        time.sleep(1)
        self.main_form.ReleaseMouseInput(coords=(703, 334))
        self.main_form.TreeView32.Wait('ready', 30, .5)
        if case['type'] == 'WN':
            node = self.main_form.TreeView2.GetItem(u"\工程列表\Demo\SG-1标\中铁七局\中心实验室\钢筋试验报告")
            node.Click()
            node.Click(button='right')
            time.sleep(1)
            self.main_form.ClickInput(coords=(250, 210), wheel_dist=.5)
            editor = timings.WaitUntilPasses(20, .5, lambda: self.app.window_(title_re=u'.*资料编辑器.*'))
            editor.Wait('ready', 30, 1)
            time.sleep(3)
            editor.GetFocus()
            BringWindowToTop(editor.handle)
            editor.ClickInput(coords=(0, 0), button='right', wheel_dist=.5)
            self.wtbh = time.strftime("%Y%m%d%H%M%S", time.localtime())
            # 设置委托编号
            editor.toolStrip2.ClickInput(coords=(544, 120), double='True', wheel_dist=.5)
            editor.TypeKeys(self.wtbh)
            # 设置钢筋直径
            editor.toolStrip2.ClickInput(coords=(574, 303), double='True', wheel_dist=.5)
            editor.TypeKeys(case['zj'])
            # 混动条拉到最下方
            editor.PressMouseInput(coords=(1347, 209))
            editor.MoveMouse(coords=(1347, 609))
            editor.ReleaseMouseInput(coords=(1347, 809))
            # 输入制件日期
            editor.ClickInput(coords=(328, 398), wheel_dist=.5)
            editor.TypeKeys(time.strftime("%Y%m%d", time.localtime()))

            # 切换到报告页面，并输入报告日期
            editor.ClickInput(coords=(345, 670), double='True', wheel_dist=.5)
            editor.ClickInput(coords=(523, 325), double='True', wheel_dist=.5)
            editor.TypeKeys(time.strftime("%Y%m%d", time.localtime()))
        else:
            node = self.main_form.TreeView2.GetItem(u"\工程列表\Demo\SG-1标\中铁七局\中心实验室\混凝土检查试件抗压强度试验报告三级配")
            node.Click()
            self.main_form.ClickInput(coords=(51, 64), wheel_dist=.5)
            editor = timings.WaitUntilPasses(20, .5, lambda: self.app.window_(title_re=u'.*资料编辑器.*'))
            editor.Wait('ready', 30, 1)
            time.sleep(1)
            editor.GetFocus()
            BringWindowToTop(editor.handle)
            editor.ClickInput(coords=(0, 0), button='right', wheel_dist=.5)
            self.wtbh = time.strftime("%Y%m%d%H%M%S", time.localtime())

            # 设置委托编号
            editor.toolStrip2.ClickInput(coords=(184, 153), double='True', wheel_dist=.5)
            editor.TypeKeys(self.wtbh)

	    #输入强度等级
            editor.toolStrip2.ClickInput(coords=(116, 290), double='True', wheel_dist=.5)
            editor.TypeKeys(case['qddj'])

            #设置试块尺寸
            editor.toolStrip2.ClickInput(coords=(210, 207), double='True')
            time.sleep(.2)
            editor.ClickInput(coords=(210, 321), double='True', wheel_dist=.5)

	   


            #输入制件日期
            editor.toolStrip2.ClickInput(coords=(678, 207), double='True', wheel_dist=.5)
            d3 = datetime.datetime.now() + datetime.timedelta(days=-28)
            editor.TypeKeys(d3.strftime("%Y%m%d"))

            #切换到委托单页面，将 混动条拉到最下方
            editor.ClickInput(coords=(332, 665), wheel_dist=.5)
            editor.toolStrip2.ClickInput(coords=(488, 280), double='True', wheel_dist=.5)
            time.sleep(1)
            editor.ClickInput(coords=(487, 395), double='True', wheel_dist=.5)

            #切换到报告页面，并输入报告日期
            editor.ClickInput(coords=(670, 665), double='True', wheel_dist=.5)
            editor.ClickInput(coords=(551, 260), double='True', wheel_dist=.5)
            editor.TypeKeys(time.strftime("%Y%m%d", time.localtime()))
        # 点击保存按钮
        editor.toolStrip12.ClickInput(coords=(57, 16), wheel_dist=.5)
        self.app[u'提示'].Wait('ready', 10, .5)
        self.app[u'提示'].Button.Click()
        time.sleep(3)
        # editor.CloseClick()
        self.app.kill_()

    def step3(self, case):
        """
        采集系统开始。。。

        """

        if case['type'] == 'WN':
            self.app.start_(config.app_cjWN_path, timeout=20, retry_interval=.5)
        else:
            self.app.start_(config.app_cjYL_path, timeout=20, retry_interval=.5)
        window = timings.WaitUntilPasses(20, .5, lambda: self.app.window_(title_re=u'.*用户登录.*'))
        # window.print_control_identifiers()
        window.Wait('ready', 20, .5)
        window.Edit2.TypeKeys(config.teddy_user_name)
        window.Edit1.TypeKeys(config.teddy_password)
        time.sleep(.6)
        timings.WaitUntil(30, 0.5, window.Button1.IsEnabled, True)
        window.Button1.Click()

        self.win1 = timings.WaitUntilPasses(20, .5, lambda: self.app.window_(title_re=u'.*选取就绪试验编号.*'))
        self.win1.Wait('ready', 100, .5)
        self.win1.TreeView.Wait('30', .5)
        nodepath = u'\\待做试验项目\\' + unicode(self.wtbh, "ascii")
        node = self.win1.TreeView2.GetItem(nodepath)
        self.win1.TreeView2.Select(nodepath)
        rct = node.Rectangle()
        self.win1.SysTreeView32.ClickInput(coords=(rct.left - 5, rct.top + 5), wheel_dist=.5)
        self.win1.Button.Click()
        self.mainform = timings.WaitUntilPasses(30, .5, lambda: self.app.window_(title_re=u'.*铁路试验实时采集系统.*'))
        self.mainform.Wait('ready', 20, 1)
        time.sleep(1)
        self.mainform.Button1.Click()
        time.sleep(1)

    def test_system(self):
        """


        """
        for case in self.case_list:
            self.step1()
            self.step2(case)
            self.step3(case)
            try:
                common_help.send_test_data(self.app, case, config.send_port, self.mainform, self.wtbh)
            except Exception, e:
                print 'error glob', e
            self.app.kill_()
            common_help.check_test_data(case, self.wtbh)
            print 'test [' + case['wtbh'] + '] end!'
            




    if __name__ == '__main__':
        unittest.main()