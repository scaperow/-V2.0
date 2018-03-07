# coding: GBK
import pymssql
import json
import serial
from pywinauto.findwindows import find_windows
import time
import os



def check_test_data(test_item=None, wtbh=''):
    """



    :rtype : object
    :type test_item: object
    :param test_item:
    :param wtbh:
    :raise (NameError, "连接数据库失败"):
    """

   
    conn = pymssql.connect(host='112.124.99.146', user='sygldb_kingrocket_f ', password='wdxlzyn@#830',
                           database='SYGLDB_Demo', charset="utf8")

    cur = conn.cursor()
    if not cur:
        raise (NameError, "连接数据库失败")

    sql = "SELECT data  FROM dbo.sys_document WHERE WTBH ='" + wtbh + "'"
    cur.execute(sql)
    result = cur.fetchall()
    dump = json.dumps(result)
    item = json.loads(dump)
    #print item
    items = json.loads(item[0][0])
    if items is None:
        print "no value upload!"

    else:
        if test_item['check'] is not None:
            for check_item in test_item['check']:
                value = get_cell_value(doc=items, sheet_id=check_item['sheet_id'], cell_name=check_item['cell_name'])
                if value is None:
                    print "value is none"
                    continue
                else:
                    assert float(check_item['min_value']) <= float(value) <= float(check_item['max_value'])
                    print value, 'passed'
    
    cur.close()
    conn.close()


def get_cell_value(doc, sheet_id, cell_name):
    if doc is not None:
        for sheet in doc["Sheets"]:
            sheet_str = json.dumps(sheet)
            sheet = json.loads(sheet_str)

            if sheet_id in sheet["ID"]:
                cell_str = json.dumps(sheet["Cells"])
                cell = json.loads(cell_str)
                i = 0
                j = len(cell)
                while i < j:
                    if cell_name in cell[i]["Name"]:
                        return cell[i]["Value"]
                    i += 1


def send_test_data(app, test_item, send_port, window, wtbh):
    """

    :param app:
    :param test_item:
    :param send_port:
    :param window:
    :param wtbh:
    """
    test_folder = os.path.abspath(os.path.split(__file__)[0])

    flag = False
    f = None
    t = None
    try:
        f = open(test_item['file'])
        t = serial.Serial(send_port, 9600)
        flag = True
    except IOError:
        print 'open serial port error: ' + send_port
    if not flag:
        f.close()
        return
    try:
        for line in f.readlines():
            items = json.loads(line)
            i = 0
            j = len(items)
            while i < j:
                if items[i]["Value"] is None:
                    print "none"
                    continue
                else:
                    item = repr(items[i]["Value"])
                    #print item
                t.write('a' + item + 'b')

                if find_windows(title_re=u'输入断后标距'):
                    app.window_(title_re=u'输入断后标距').Edit1.TypeKeys(test_item['dhbj'])
                    app.window_(title_re=u'输入断后标距').Button1.Click()

                i += 1
                time.sleep(0.01)

    except IOError, e:
        print e
    t.close()
    f.close()
    #print 'closed the file and serial port'
    time.sleep(1)
    
    if find_windows(title_re=u'输入断后标距'):
        app.window_(title_re=u'输入断后标距').Edit1.TypeKeys(test_item['dhbj'])
        app.window_(title_re=u'输入断后标距').Button1.Click()
    time.sleep(1)
    img = window.CaptureAsImage()
    imgURl = unicode(test_folder, encoding="GBK") + '\picture\\'+test_item['type']+'\\'+wtbh + '.gif'
    img.save(imgURl)



if __name__ == '__main__':
    case = {'type': 'WN', 'file': 'D:\\test\\1.txt', 'check': [
        {'sheet_id': "270a1da6-2045-405a-ae77-18c0c98c1edd", 'cell_name': 'D29', 'min_value': '122',
         'max_value': '123'},{'sheet_id': "270a1da6-2045-405a-ae77-18c0c98c1edd", 'cell_name': 'G29', 'min_value': '120',
         'max_value': '121'}]}
    check_test_data(case, '20140801095404')

    #send_test_data(None, case, 'COM1')