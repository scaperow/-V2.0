# coding: GBK
__author__ = 'Administrator'
from xml.dom import minidom
import os
import pymssql
import json
import base64
import zlib

def get_test_cases():
    """
    :return:
    """
    test_folder = os.path.abspath(os.path.split(__file__)[0])
    doc = minidom.parse(test_folder + '\TestCases\TestCases.xml')
    root = doc.documentElement
    user_nodes = root.getElementsByTagName('case')
    case_list = []

    for node in user_nodes:
        case = {'server': node.getAttribute('server'), 'type': node.getAttribute('type'), 'zj': node.getAttribute('zj'),
                'db': node.getAttribute('db'), 'dhbj': node.getAttribute('dhbj'), 'qddj': node.getAttribute('qddj'),
                'wtbh': node.getAttribute('wtbh'), 'check': []}
        check_list = node.getElementsByTagName('check')
        for check_item in check_list:
            check = {'sheet_id': check_item.getAttribute('sheet_id'), 'cell_name': check_item.getAttribute('cell_name'),
                     'min_value': check_item.getAttribute('min_value'),
                     'max_value': check_item.getAttribute('max_value')}
            case['check'].append(check)

        if node.getAttribute('active') == '1':
            f = unicode(test_folder, encoding="GBK") + '\TestCases\\' + case['type'] + '\\' + case['wtbh'] + '.txt'
            case['file'] = f
            download_test_data(case)
            case_list.append(case)
    return case_list


def download_test_data(case):
    if not os.path.exists(case['file']):
        conn = pymssql.connect(host=case['server'], user='sygldb_kingrocket_f ', password='wdxlzyn@#830',
                               database=case['db'], charset="utf8")

        cur = conn.cursor()
        if not cur:
            raise (NameError, "连接数据库失败")
        else:
            print 'connected'

        sql = "SELECT RealTimeData FROM dbo.sys_test_data WHERE WTBH ='" + case['wtbh'] + "'" + "ORDER BY SerialNumber"
        cur.execute(sql)
        resList = cur.fetchall()

        file = open(case['file'], 'w')
        try:
            i = 0
            j = len(resList)
            for dump in resList:
                if i < j:
                    dump = json.dumps(dump)
                    str = base64.b64decode(dump)
                    dump = zlib.decompress(str, 16+zlib.MAX_WBITS)
                    print >>file, dump
                    i += 1
        except Exception, e:
            dump = json.dumps(resList)
            str = json.loads(dump)
            i = 0
            j = len(str)
            for line in str:
                if i < j:
                    print >> file, line[0]
                    i += 1



if __name__ == '__main__':
    get_test_cases()