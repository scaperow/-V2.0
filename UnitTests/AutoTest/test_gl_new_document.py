# coding=GBK
import unittest
from pywinauto import application
from pywinauto import timings
from pywinauto.win32functions import *
import time


class NewDocumentTest(unittest.TestCase):
    def setUp(self):
        self.app = application.Application()
        self.app_path = "G:\�ͻ���\�ͻ��ˣ��¼ܹ�-Demo��\\��·������Ϣ����ϵͳ.exe".decode('GBK')
        self.user_name = u'������'
        self.password = '6725638'
        self.app.start_(self.app_path, timeout=20, retry_interval=.5)
        login_window = timings.WaitUntilPasses(120, .5, lambda: self.app.window_(title_re=u'.*�û���¼.*'))
        login_window.Wait('ready', 20, 1)

        login_window.Edit1.TypeKeys(self.user_name)
        login_window.Edit2.TypeKeys(self.password)
        timings.WaitUntil(10, 0.5, login_window.Button1.IsEnabled, True)
        login_window.Button1.Click()

        self.main_form = timings.WaitUntilPasses(30, .5, lambda: self.app.window_(title_re=u'.*��·������Ϣ����ϵͳ.*'))
        time.sleep(2)
        self.main_form.Wait('ready', 40, 1)
        time.sleep(3)
        self.main_form.GetFocus()
        BringWindowToTop(self.main_form.handle)

    def tearDown(self):
        self.app.kill_()
        pass

    def test_new_document(self):
        self.main_form.PressMouseInput(coords=(11, 115))
        self.main_form.MoveMouse(coords=(11, 117))
        self.main_form.Main_TabControl.ClickInput(coords=(100, 45), double=True, wheel_dist=.5)

        win1 = timings.WaitUntilPasses(20, .5, lambda: self.app.window_(title_re=u'.*���������б�.*'))
        win1.Wait('ready', 60, 1)
        BringWindowToTop(win1.handle)
        win1.Close()
        self.main_form.PressMouseInput(coords=(544, 16))
        self.main_form.ReleaseMouseInput(coords=(544, 16))
        self.main_form.TreeView2.Wait('ready', 20, 1)
        node = self.main_form.TreeView2.GetItem(u"\�����б�\Demo\SG-1��\�����߾�\����ʵ����\�ֽ����鱨��")
        node.Click()
        node.Click(button='right')
        time.sleep(1)
        self.main_form.ClickInput(coords=(250, 210), wheel_dist=.5)

        editor = timings.WaitUntilPasses(20, .5, lambda: self.app.window_(title_re=u'.*���ϱ༭��.*'))
        editor.Wait('ready', 120, 1)
        time.sleep(3)
        editor.GetFocus()
        BringWindowToTop(editor.handle)
        editor.ClickInput(coords=(0,0), button='right',wheel_dist=.5)
        self.wtbh = time.strftime("%Y%m%d%H%M%S", time.localtime())
        # ����ί�б��
        editor.toolStrip2.ClickInput(coords=(544, 120), double='True', wheel_dist=.5)
        editor.TypeKeys(self.wtbh)
        #�춯���������·�
        editor.PressMouseInput(coords=(1347, 209))
        editor.MoveMouse(coords=(1347, 809))
        editor.ReleaseMouseInput(coords=(1347, 809))
        #�����Ƽ�����
        editor.ClickInput(coords=(328, 398), double='True', wheel_dist=.5)
        editor.TypeKeys(time.strftime("%Y%m%d", time.localtime()))
        #�л�������ҳ�棬�����뱨������
        editor.ClickInput(coords=(345, 670), double='True', wheel_dist=.5)
        editor.ClickInput(coords=(523, 325), double='True', wheel_dist=.5)
        editor.TypeKeys(time.strftime("%Y%m%d", time.localtime()))
        #������水ť
        editor.toolStrip12.ClickInput(coords=(57, 16), wheel_dist=.5)
        time.sleep(3)
        self.app[u'��ʾ'].Button.Click()
        time.sleep(5)
        editor.Close()


if __name__ == '__main__':
    unittest.main()