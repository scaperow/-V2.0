#coding=GBK
import unittest
from pywinauto import application
from pywinauto import timings
import time


class LoginTest(unittest.TestCase):
    "测试登录功能"

    ##初始化工作
    def setUp(self):
        self.app_path = "G:\客户端\客户端（新架构-Demo）\\铁路试验信息管理系统.exe".decode('GBK')
        self.user_name = u'刘晓明'
        self.password = '6725638'

    #退出清理工作
    def tearDown(self):
        pass

    def test_login(self):
        print 'test login'
        app = application.Application()
        app.start_(self.app_path, timeout=20, retry_interval=.5)
        login_window = timings.WaitUntilPasses(120, .5, lambda: app.window_(title_re=u'.*用户登录.*'))
        login_window.Wait('ready', 20, 1)
        self.assertEqual(login_window.ProcessID(), app.process)

        login_window.Edit1.TypeKeys(self.user_name)
        login_window.Edit2.TypeKeys(self.password)
        timings.WaitUntil(10, 0.5, login_window.Button1.IsEnabled, True)
        login_window.Button1.Click()

        main_form = timings.WaitUntilPasses(30, .5, lambda: app.window_(title_re=u'.*铁路试验信息管理系统.*'))
        main_form.Wait('ready', 30, 1)
        main_form.GetFocus()
        time.sleep(1)
        self.assertEqual(main_form.GetShowState(), 3)
        app.kill_()

    def test_login_fail(self):
        print 'test login fail'
        app = application.Application()
        app.start_(self.app_path, timeout=20, retry_interval=.5)
        login_window = timings.WaitUntilPasses(120, .5, lambda: app.window_(title_re=u'.*用户登录.*'))
        login_window.Wait('ready', 20, 1)
        self.assertEqual(login_window.ProcessID(), app.process)

        login_window.Edit1.TypeKeys('aaa')
        login_window.Edit2.TypeKeys(self.password)
        timings.WaitUntil(10, 0.5, login_window.Button1.IsEnabled, True)
        login_window.Button1.Click()
        time.sleep(.5)
        app[u'提示'].Button.Click()

        login_window = timings.WaitUntilPasses(120, .5, lambda: app.window_(title_re=u'.*用户登录.*'))
        login_window.Wait('ready', 20, 1)
        self.assertEqual(login_window.ProcessID(), app.process)
        app.kill_()


if __name__ == '__main__':
    unittest.main()
