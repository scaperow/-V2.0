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
        login_window = timings.WaitUntilPasses(120, .5, lambda: self.app.window_(title_re=u'.*�û���¼.*'))
        login_window.Wait('ready', 20, 1)

        login_window.Edit1.TypeKeys(config.app_user_name)
        login_window.Edit2.TypeKeys(config.app_password)
        timings.WaitUntil(20, 0.5, login_window.Button1.IsEnabled, True)
        time.sleep(1)
        login_window.Button1.Click()

        self.main_form = timings.WaitUntilPasses(30, .5, lambda: self.app.window_(title_re=u'.*��·������Ϣ����ϵͳ.*'))
        time.sleep(2)
        self.main_form.Wait('ready', 120, 1)
        time.sleep(3)
        self.main_form.GetFocus()
        BringWindowToTop(self.main_form.handle)

    def step2(self, case):
        self.main_form.PressMouseInput(coords=(11, 115))
        self.main_form.MoveMouse(coords=(11, 117))
        self.main_form.Main_TabControl.ClickInput(coords=(100, 45), double=True, wheel_dist=.5)

        win1 = timings.WaitUntilPasses(20, .5, lambda: self.app.window_(title_re=u'.*���������б�.*'))
        win1.Wait('ready', 60, 1)
        BringWindowToTop(win1.handle)
        win1.Close()
        self.main_form.PressMouseInput(coords=(703, 334))
        time.sleep(1)
        self.main_form.ReleaseMouseInput(coords=(703, 334))
        self.main_form.TreeView32.Wait('ready', 30, .5)
        if case['type'] == 'WN':
            node = self.main_form.TreeView2.GetItem(u"\�����б�\Demo\SG-1��\�����߾�\����ʵ����\�ֽ����鱨��")
            node.Click()
            node.Click(button='right')
            time.sleep(1)
            self.main_form.ClickInput(coords=(250, 210), wheel_dist=.5)
            editor = timings.WaitUntilPasses(20, .5, lambda: self.app.window_(title_re=u'.*���ϱ༭��.*'))
            editor.Wait('ready', 30, 1)
            time.sleep(3)
            editor.GetFocus()
            BringWindowToTop(editor.handle)
            editor.ClickInput(coords=(0, 0), button='right', wheel_dist=.5)
            self.wtbh = time.strftime("%Y%m%d%H%M%S", time.localtime())
            # ����ί�б��
            editor.toolStrip2.ClickInput(coords=(544, 120), double='True', wheel_dist=.5)
            editor.TypeKeys(self.wtbh)
            # ���øֽ�ֱ��
            editor.toolStrip2.ClickInput(coords=(574, 303), double='True', wheel_dist=.5)
            editor.TypeKeys(case['zj'])
            # �춯���������·�
            editor.PressMouseInput(coords=(1347, 209))
            editor.MoveMouse(coords=(1347, 609))
            editor.ReleaseMouseInput(coords=(1347, 809))
            # �����Ƽ�����
            editor.ClickInput(coords=(328, 398), wheel_dist=.5)
            editor.TypeKeys(time.strftime("%Y%m%d", time.localtime()))

            # �л�������ҳ�棬�����뱨������
            editor.ClickInput(coords=(345, 670), double='True', wheel_dist=.5)
            editor.ClickInput(coords=(523, 325), double='True', wheel_dist=.5)
            editor.TypeKeys(time.strftime("%Y%m%d", time.localtime()))
        else:
            node = self.main_form.TreeView2.GetItem(u"\�����б�\Demo\SG-1��\�����߾�\����ʵ����\����������Լ���ѹǿ�����鱨��������")
            node.Click()
            self.main_form.ClickInput(coords=(51, 64), wheel_dist=.5)
            editor = timings.WaitUntilPasses(20, .5, lambda: self.app.window_(title_re=u'.*���ϱ༭��.*'))
            editor.Wait('ready', 30, 1)
            time.sleep(1)
            editor.GetFocus()
            BringWindowToTop(editor.handle)
            editor.ClickInput(coords=(0, 0), button='right', wheel_dist=.5)
            self.wtbh = time.strftime("%Y%m%d%H%M%S", time.localtime())

            # ����ί�б��
            editor.toolStrip2.ClickInput(coords=(184, 153), double='True', wheel_dist=.5)
            editor.TypeKeys(self.wtbh)

	    #����ǿ�ȵȼ�
            editor.toolStrip2.ClickInput(coords=(116, 290), double='True', wheel_dist=.5)
            editor.TypeKeys(case['qddj'])

            #�����Կ�ߴ�
            editor.toolStrip2.ClickInput(coords=(210, 207), double='True')
            time.sleep(.2)
            editor.ClickInput(coords=(210, 321), double='True', wheel_dist=.5)

	   


            #�����Ƽ�����
            editor.toolStrip2.ClickInput(coords=(678, 207), double='True', wheel_dist=.5)
            d3 = datetime.datetime.now() + datetime.timedelta(days=-28)
            editor.TypeKeys(d3.strftime("%Y%m%d"))

            #�л���ί�е�ҳ�棬�� �춯���������·�
            editor.ClickInput(coords=(332, 665), wheel_dist=.5)
            editor.toolStrip2.ClickInput(coords=(488, 280), double='True', wheel_dist=.5)
            time.sleep(1)
            editor.ClickInput(coords=(487, 395), double='True', wheel_dist=.5)

            #�л�������ҳ�棬�����뱨������
            editor.ClickInput(coords=(670, 665), double='True', wheel_dist=.5)
            editor.ClickInput(coords=(551, 260), double='True', wheel_dist=.5)
            editor.TypeKeys(time.strftime("%Y%m%d", time.localtime()))
        # ������水ť
        editor.toolStrip12.ClickInput(coords=(57, 16), wheel_dist=.5)
        self.app[u'��ʾ'].Wait('ready', 10, .5)
        self.app[u'��ʾ'].Button.Click()
        time.sleep(3)
        # editor.CloseClick()
        self.app.kill_()

    def step3(self, case):
        """
        �ɼ�ϵͳ��ʼ������

        """

        if case['type'] == 'WN':
            self.app.start_(config.app_cjWN_path, timeout=20, retry_interval=.5)
        else:
            self.app.start_(config.app_cjYL_path, timeout=20, retry_interval=.5)
        window = timings.WaitUntilPasses(20, .5, lambda: self.app.window_(title_re=u'.*�û���¼.*'))
        # window.print_control_identifiers()
        window.Wait('ready', 20, .5)
        window.Edit2.TypeKeys(config.teddy_user_name)
        window.Edit1.TypeKeys(config.teddy_password)
        time.sleep(.6)
        timings.WaitUntil(30, 0.5, window.Button1.IsEnabled, True)
        window.Button1.Click()

        self.win1 = timings.WaitUntilPasses(20, .5, lambda: self.app.window_(title_re=u'.*ѡȡ����������.*'))
        self.win1.Wait('ready', 100, .5)
        self.win1.TreeView.Wait('30', .5)
        nodepath = u'\\����������Ŀ\\' + unicode(self.wtbh, "ascii")
        node = self.win1.TreeView2.GetItem(nodepath)
        self.win1.TreeView2.Select(nodepath)
        rct = node.Rectangle()
        self.win1.SysTreeView32.ClickInput(coords=(rct.left - 5, rct.top + 5), wheel_dist=.5)
        self.win1.Button.Click()
        self.mainform = timings.WaitUntilPasses(30, .5, lambda: self.app.window_(title_re=u'.*��·����ʵʱ�ɼ�ϵͳ.*'))
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