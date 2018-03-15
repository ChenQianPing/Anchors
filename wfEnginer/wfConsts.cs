using System;
using System.Collections.Generic;
using System.Text;

namespace wfEnginer
{
    public class wfConsts
    {
        public const string NODE_START = "开始";
        public const string NODE_END = "结束";
        public const string NODE_NORMAL = "节点";
        public const string NODE_OR_BRANCH = "或分支";
        public const string NODE_IF_BRANCH = "条件分支";
        public const string NODE_MERGE_OR = "或聚合";            //只要有一个条件满足就可以继续流程
        public const string NODE_MERGE_AND = "与聚合";           //需要等待多个前置条件满足才继续流程
        public const string NODE_MERGE_VOTE = "投票聚合";        //满足N个条件中的M个条件就继续流程

        /***********************START****
         * 作者    :ruijc
         * 修改原因:流程申批退回到流程审批发起节点后,将流程暂停,
         *         待发起人修改申请信息后,可重启流程.
         * 修改时间:2008-03-04
         * 说明:本段代码为新增加的
         **/
        public const string PROCESS_STATUS_PAUSE = "0";     //流程暂停
        /***********************END*****/

        public const string PROCESS_STATUS_FINISHED = "1";     //流程结束
        public const string PROCESS_STATUS_PROCESSING = "2";     //流程流转中
        public const string PROCESS_STATUS_CANCEL = "3";     //流被取消流转

        public const string TASK_STATUS_PENDING = "待同步";
        public const string TASK_STATUS_WAIT = "待处理";
        public const string TASK_STATUS_END = "已处理";
        public const string TASK_STATUS_CANCEL = "被取消";

        public const string RUN_STATE_ACCEPT = "同意";
        public const string RUN_STATE_REJECT = "否决";
        public const string RUN_STATE_CANEL = "退回";
        public const string RUN_STATE_MANUAL = "手动";
        public const string RUN_STATE_CANELED = "被退回";

        public const string ASSIGN_TYPE_USER = "user";
        public const string ASSIGN_TYPE_ROLE = "role";
        public const string ASSIGN_TYPE_DEPT = "dept";


    }
}
