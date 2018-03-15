using System;
using System.Collections.Generic;
using System.Text;

namespace wfEnginer
{
    public class wfConsts
    {
        public const string NODE_START = "��ʼ";
        public const string NODE_END = "����";
        public const string NODE_NORMAL = "�ڵ�";
        public const string NODE_OR_BRANCH = "���֧";
        public const string NODE_IF_BRANCH = "������֧";
        public const string NODE_MERGE_OR = "��ۺ�";            //ֻҪ��һ����������Ϳ��Լ�������
        public const string NODE_MERGE_AND = "��ۺ�";           //��Ҫ�ȴ����ǰ����������ż�������
        public const string NODE_MERGE_VOTE = "ͶƱ�ۺ�";        //����N�������е�M�������ͼ�������

        /***********************START****
         * ����    :ruijc
         * �޸�ԭ��:���������˻ص�������������ڵ��,��������ͣ,
         *         ���������޸�������Ϣ��,����������.
         * �޸�ʱ��:2008-03-04
         * ˵��:���δ���Ϊ�����ӵ�
         **/
        public const string PROCESS_STATUS_PAUSE = "0";     //������ͣ
        /***********************END*****/

        public const string PROCESS_STATUS_FINISHED = "1";     //���̽���
        public const string PROCESS_STATUS_PROCESSING = "2";     //������ת��
        public const string PROCESS_STATUS_CANCEL = "3";     //����ȡ����ת

        public const string TASK_STATUS_PENDING = "��ͬ��";
        public const string TASK_STATUS_WAIT = "������";
        public const string TASK_STATUS_END = "�Ѵ���";
        public const string TASK_STATUS_CANCEL = "��ȡ��";

        public const string RUN_STATE_ACCEPT = "ͬ��";
        public const string RUN_STATE_REJECT = "���";
        public const string RUN_STATE_CANEL = "�˻�";
        public const string RUN_STATE_MANUAL = "�ֶ�";
        public const string RUN_STATE_CANELED = "���˻�";

        public const string ASSIGN_TYPE_USER = "user";
        public const string ASSIGN_TYPE_ROLE = "role";
        public const string ASSIGN_TYPE_DEPT = "dept";


    }
}
