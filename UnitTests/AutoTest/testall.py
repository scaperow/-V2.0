# coding=UTF-8
import unittest
import os
import sys

sys.path.append(".")
excludes = ['test_cj', 'test_gl_login', 'test_gl_new_document']


def run_tests():
    test_folder = os.path.abspath(os.path.split(__file__)[0])
    sys.path.append(test_folder)

    for root, dirs, files in os.walk(test_folder):
        test_modules = [
            file.replace('.py', '') for file in files if
            file.startswith('test_') and
            file.endswith('.py')]
        test_modules = [mod for mod in test_modules if mod.lower() not in excludes]
        for mod in test_modules:
            imported_mod = __import__(mod, globals(), locals())

            globals().update(imported_mod.__dict__)

    unittest.main()


if __name__ == '__main__':
    run_tests()